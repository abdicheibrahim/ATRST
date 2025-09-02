using ProjetAtrst.DTO;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Project;
using ProjetAtrst.ViewModels.ProjectMembership;
using ProjetAtrst.ViewModels.ProjectRequests;
using System.Text.Json;

namespace ProjetAtrst.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRequestRepository _projectRequestRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProjectService(IUnitOfWork unitOfWork, IProjectRequestRepository projectRequestRepository, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _projectRequestRepository = projectRequestRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        //new-------
        public async Task CreateProjectAsync(ProjectCreateViewModel model, string researcherId)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrEmpty(researcherId))
                throw new ArgumentException("Researcher ID cannot be null or empty.", nameof(researcherId));

            try
            {
                // معالجة المراجع
                var referencesList = ProcessReferences(model.References);
                string referencesJson = JsonSerializer.Serialize(referencesList);

                // إنشاء المشروع
                var project = new Project
                {
                    Title = model.Title?.Trim(),
                    IsAcceptingJoinRequests = model.IsAcceptingJoinRequests,

                    // معالجة الكلمات المفتاحية
                    Keywords = ProcessKeywords(model.Keywords),

                    // الحقول الأخرى مع معالجة null
                    Domain = model.Domain?.Trim(),
                    Axis = model.Axis?.Trim(),
                    Theme = model.Theme?.Trim(),
                    Nature = model.Nature?.Trim(),
                    TRL = model.TRL?.Trim(),
                    PNR = model.PNR?.Trim(),
                    DurationInMonths = model.DurationInMonths,
                    HostInstitution = model.HostInstitution?.Trim(),

                    // الوصف مع معالجة null
                    CurrentState = model.CurrentState?.Trim(),
                    Motivation = model.Motivation?.Trim(),
                    Methodology = model.Methodology?.Trim(),

                    SocioEconomicPartner = model.SocioEconomicPartner?.Trim(),
                    ExpectedResults = model.ExpectedResults?.Trim(),
                    TargetSectors = model.TargetSectors?.Trim(),
                    Impact = model.Impact?.Trim(),

                    ReferencesJson = referencesJson,
                    CreationDate = DateTime.UtcNow,
                    LastActivity = DateTime.UtcNow
                };

                // حفظ المشروع
                await _unitOfWork.Projects.AddAsync(project);
                await _unitOfWork.SaveAsync();

                // إنشاء العضوية
                var membership = new ProjectMembership
                {
                    ProjectId = project.Id,
                    UserId = researcherId,
                    Role = Role.Leader,
                    JoinedAt = DateTime.UtcNow
                };

                await _unitOfWork.ProjectMemberships.AddAsync(membership);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                // يمكن تسجيل الخطأ هنا
                // _logger?.LogError(ex, "Error creating project for researcher {ResearcherId}", researcherId);
                throw new InvalidOperationException("Failed to create project.", ex);
            }
        }

        // ✅ طريقة خاصة لمعالجة المراجع
        private List<string> ProcessReferences(string references)
        {
            if (string.IsNullOrWhiteSpace(references))
                return new List<string>();

            return references.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(r => r.Trim())
                            .Where(r => !string.IsNullOrWhiteSpace(r))
                            .ToList();
        }

        // ✅ طريقة خاصة لمعالجة الكلمات المفتاحية
        private List<string> ProcessKeywords(List<string> keywords)
        {
            if (keywords == null || !keywords.Any())
                return new List<string>();

            return keywords.Where(k => !string.IsNullOrWhiteSpace(k))
                          .Select(k => k.Trim())
                          .Distinct() // إزالة التكرار
                          .ToList();
        }

        // ✅ طريقة إضافية مفيدة للتحقق من صحة المشروع
        public async Task<bool> CanUserCreateProjectAsync(string researcherId)
        {
            if (string.IsNullOrEmpty(researcherId))
                return false;

            try
            {
                // يمكنك إضافة قواعد عمل هنا
                // مثلاً: التحقق من عدد المشاريع الحالية للمستخدم
                // var currentProjects = await _unitOfWork.Projects.CountAsync(p => p.Members.Any(m => m.ResearcherId == researcherId));
               //  return currentProjects < MAX_PROJECTS_LIMIT;

                return true; // مؤقتاً
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProjectListViewModel>> GetProjectsForResearcherAsync(string researcherId)
        {
            var memberships = await _unitOfWork.ProjectMemberships
                .GetAllByResearcherWithProjectsAsync(researcherId);

            return memberships.Select(pm => new ProjectListViewModel
            {
                Id = pm.Project.Id,
                Title = pm.Project.Title,
                CreationDate = pm.Project.CreationDate,
                Status = pm.Project.ProjectStatus.ToString(),
                IsAcceptingJoinRequests = pm.Project.IsAcceptingJoinRequests,
                LastActivity = pm.Project.LastActivity,
                LogoPath = string.IsNullOrEmpty(pm.Project.LogoPath)
               ? "/images/default-project.png"
               : pm.Project.LogoPath,
                Role = pm.Role switch
                {
                    Role.Leader => Role.Leader,
                    Role.Member => Role.Member,
                    _ => Role.Viewer,

                }


            }).ToList();
        }
        public async Task<ProjectDetailsViewModel?> GetProjectDetailsForResearcherAsync(string researcherId, int projectId)
        {
            var membership = await _unitOfWork.ProjectMemberships
                .GetByResearcherAndProjectAsync(researcherId, projectId);

            if (membership == null) return null;

            var project = membership.Project;

            return new ProjectDetailsViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                CreationDate = project.CreationDate,
                Status = project.ProjectStatus.ToString(),
                Role = membership.Role 
            };
        }
        //public async Task<ProjectEditViewModel?> GetProjectForEditAsync(string researcherId, int projectId)
        //{
        //    var membership = await _unitOfWork.ProjectMemberships
        //        .GetByResearcherAndProjectAsync(researcherId, projectId);

        //    if (membership == null || membership.Role != Role.Leader)
        //        return null;

        //    var project = membership.Project;

        //    return new ProjectEditViewModel
        //    {
        //        Id = project.Id,
        //        Title = project.Title,
        //        Description = project.Description,
        //        CurrentLogoPath = string.IsNullOrEmpty(project.LogoPath)
        //        ? "/images/default-project.png"
        //        : project.LogoPath

        //    };
        //}
        //public async Task<bool> UpdateProjectAsync(string researcherId, ProjectEditViewModel model)
        //{
        //    var membership = await _unitOfWork.ProjectMemberships
        //        .GetByResearcherAndProjectAsync(researcherId, model.Id);

        //    if (membership == null || membership.Role != Role.Leader)
        //        return false;

        //    var project = membership.Project;
        //    project.Title = model.Title;
        //    project.Description = model.Description;
        //    project.LastActivity = DateTime.UtcNow;
        //    project.LogoPath = model.CurrentLogoPath; // احفظ المسار القديم إذا كان موجودًا

        //    // ✅ معالجة الصورة إذا تم رفعها
        //    if (model.LogoFile != null && model.LogoFile.Length > 0)
        //    {
        //        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "ProjectImages");
        //        Directory.CreateDirectory(uploadsFolder); // تأكد أن المجلد موجود أو يتم إنشاؤه

        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);
        //        var filePath = Path.Combine(uploadsFolder, fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await model.LogoFile.CopyToAsync(stream);
        //        }

        //        // احفظ المسار النسبي الذي يمكن استخدامه في العرض
        //        project.LogoPath = "/uploads/ProjectImages/" + fileName;
        //    }

        //    await _unitOfWork.SaveAsync();
        //    return true;
        //}

        // ------------- السيناريو الأول: Paging عادي -------------
        public async Task<(List<AvailableProjectViewModel> Projects, int TotalCount)>
            GetAvailableProjectsPageAsync(string researcherId, int pageNumber, int pageSize)
        {
            var (dtos, totalCount) = await _unitOfWork.Projects
                .GetAvailableProjectsForJoinAsync(researcherId, pageNumber, pageSize);

            var vms = dtos.Select(MapToVm).ToList();
            return (vms, totalCount);
        }

        // ------------- السيناريو الثاني: DataTables Server-Side -------------
        public async Task<DataTableResponse<AvailableProjectViewModel>> GetAvailableProjectsDataTableAsync(
            string researcherId,
            int start,
            int length,
            string? searchValue,
            string? sortColumn,
            string? sortDirection,
            string? draw)
        {
            if (length <= 0) length = 10;
            if (start < 0) start = 0;

            // الاستعلام الأساسي (بعد شروط "المتاح للانضمام")
            var baseQuery = _unitOfWork.Projects.GetAvailableProjectsQuery(researcherId);

            // إجمالي السجلات قبل البحث
            var totalRecords = await baseQuery.CountAsync();

            // تطبيق البحث
            var filteredQuery = ApplySearch(baseQuery, searchValue);

            // إجمالي بعد البحث
            var filteredRecords = await filteredQuery.CountAsync();

            // تطبيق الترتيب
            var sortedQuery = ApplySorting(filteredQuery, sortColumn, sortDirection);

            // Paging
            var page = await sortedQuery
                .Skip(start)
                .Take(length)
                .ToListAsync();

            // تحويل إلى ViewModel
            var data = page.Select(MapToVm).ToList();

            return new DataTableResponse<AvailableProjectViewModel>
            {
                Draw = draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = filteredRecords,
                Data = data
            };
        }

        // ----------------- Helpers -----------------

        private static IQueryable<AvailableProjectDto> ApplySearch(IQueryable<AvailableProjectDto> query, string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return query;

            search = search.Trim();
            return query.Where(p =>
                (p.Title ?? "").Contains(search) ||
              //  (p.Description ?? "").Contains(search) ||
                (p.LeaderFullName ?? "").Contains(search));
        }

        private static IQueryable<AvailableProjectDto> ApplySorting(
            IQueryable<AvailableProjectDto> query,
            string? sortColumn,
            string? sortDirection)
        {
            // الأعمدة المسموحة (طابق أسماء DataTables "columns[i].name")
            // مثال: name="Title" / "LeaderFullName" / "CreationDate"
            var dirDesc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);

            switch (sortColumn)
            {
                case "Title":
                    return dirDesc ? query.OrderByDescending(p => p.Title)
                                   : query.OrderBy(p => p.Title);

                case "LeaderFullName":
                    return dirDesc ? query.OrderByDescending(p => p.LeaderFullName)
                                   : query.OrderBy(p => p.LeaderFullName);

                case "CreationDate":
                    return dirDesc ? query.OrderByDescending(p => p.CreationDate)
                                   : query.OrderBy(p => p.CreationDate);

                default:
                    // ترتيب افتراضي ثابت
                    return query.OrderByDescending(p => p.CreationDate);
            }
        }

        private static AvailableProjectViewModel MapToVm(AvailableProjectDto dto) => new()
        {
            Id = dto.Id,
            Title = dto.Title,
          //  Description = dto.Description,
            CreationDate = dto.CreationDate,
            ImageUrl = dto.ImageUrl,
            LeaderId = dto.LeaderId,
            LeaderFullName = dto.LeaderFullName
        };
        public async Task<ProjectDetailsV2ViewModel?> GetProjectDetailsAsync(int projectId)
        {
            var dto = await _unitOfWork.Projects.GetProjectDetailsAsync(projectId);
            if (dto == null) return null;

            return new ProjectDetailsV2ViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CreationDate = dto.CreationDate,
                ImageUrl = dto.ImageUrl,
                LeaderId = dto.LeaderId,
                LeaderFullName = dto.LeaderFullName
            };
        }
        //--------------//
        public async Task<Project> GetByIdAsync(int id)
        {
            return await _unitOfWork.Projects.GetByIdAsync(id);
        }
        public async Task<bool> IsUserLeaderAsync(string researcherId, int projectId)
        {
            return await  _unitOfWork.ProjectMemberships.IsUserLeaderAsync(researcherId, projectId);
        }
        public async Task<List<ProjectMemberViewModel>> GetProjectMembersAsync(int projectId)
        {
            var memberships = await _unitOfWork.ProjectMemberships.GetMembersByProjectIdAsync(projectId);

            return memberships.Select(m => new ProjectMemberViewModel
            {
                Name = m.User.FullName,
                Email = m.User.Email,
                Role = m.Role,
                JoinedAt = m.JoinedAt
            }).ToList();
        }
        public async Task<List<ProjectJoinRequestViewModel>> GetJoinRequestsAsync(int projectId)
        {
            var requests = await _projectRequestRepository.GetJoinRequestsByProjectIdAsync(projectId);

            return requests.Select(r => new ProjectJoinRequestViewModel
            {
                RequestId = r.Id,
                SenderId = r.SenderId,
                SenderName = r.Sender.FullName,
                Status = r.Status,
                SentAt = r.CreatedAt
            }).ToList();
        }  
        public async Task<List<ProjectJoinRequestViewModel>> GetInvitationRequestsAsync(int projectId)
        {
            var requests = await _projectRequestRepository.GetInvitationRequestsByProjectIdAsync(projectId);

            return requests.Select(r => new ProjectJoinRequestViewModel
            {
                RequestId = r.Id,
                SenderId = r.SenderId,
                SenderName = r.Receiver.FullName,
                Status = r.Status,
                SentAt = r.CreatedAt
            }).ToList();
        }

        //--new methods for project management--//
        
        public async Task<ProjectEditViewModel?> GetProjectForEditAsync(string researcherId, int projectId)
        {
            var membership = await _unitOfWork.ProjectMemberships
                .GetByResearcherAndProjectAsync(researcherId, projectId);

            if (membership == null || membership.Role != Role.Leader)
                return null;

            var project = membership.Project;

            // تحويل المراجع من JSON إلى نص
            var references = new List<string>();
            if (!string.IsNullOrEmpty(project.ReferencesJson))
            {
                try
                {
                    references = JsonSerializer.Deserialize<List<string>>(project.ReferencesJson) ?? new List<string>();
                }
                catch
                {
                    // في حالة خطأ في التحليل، نقسم النص بالسطور
                    references = project.ReferencesJson.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(r => r.Trim())
                                                     .Where(r => !string.IsNullOrWhiteSpace(r))
                                                     .ToList();
                }
            }

            return new ProjectEditViewModel
            {
                Id = project.Id,
                Title = project.Title,
                PNR = project.PNR,
                Nature = project.Nature,
                Domain = project.Domain,
                Axis = project.Axis,
                Theme = project.Theme,
                Keywords = project.Keywords ?? new List<string>(),
                HostInstitution = project.HostInstitution,
                TRL = project.TRL,
                DurationInMonths = project.DurationInMonths,
                IsAcceptingJoinRequests = project.IsAcceptingJoinRequests,
                CurrentState = project.CurrentState,
                Motivation = project.Motivation,
                Methodology = project.Methodology,
                SocioEconomicPartner = project.SocioEconomicPartner,
                TargetSectors = project.TargetSectors,
                ExpectedResults = project.ExpectedResults,
                Impact = project.Impact,
                References = string.Join("\n", references),
                CurrentLogoPath = string.IsNullOrEmpty(project.LogoPath)
                    ? "/images/default-project.png"
                    : project.LogoPath
            };
        }

        public async Task<bool> UpdateProjectAsync(string researcherId, ProjectEditViewModel model)
        {
            var membership = await _unitOfWork.ProjectMemberships
                .GetByResearcherAndProjectAsync(researcherId, model.Id);

            if (membership == null || membership.Role != Role.Leader)
                return false;

            var project = membership.Project;

            try
            {
                // معالجة المراجع
                var referencesList = ProcessReferences(model.References);
                string referencesJson = JsonSerializer.Serialize(referencesList);

                // تحديث الحقول الأساسية
                project.Title = model.Title?.Trim();
                project.PNR = model.PNR?.Trim();
                project.Nature = model.Nature?.Trim();
                project.Domain = model.Domain?.Trim();
                project.Axis = model.Axis?.Trim();
                project.Theme = model.Theme?.Trim();
                project.Keywords = ProcessKeywords(model.Keywords);
                project.HostInstitution = model.HostInstitution?.Trim();
                project.TRL = model.TRL?.Trim();
                project.DurationInMonths = model.DurationInMonths;
                project.IsAcceptingJoinRequests = model.IsAcceptingJoinRequests;

                // تحديث الحقول الوصفية
                project.CurrentState = model.CurrentState?.Trim();
                project.Motivation = model.Motivation?.Trim();
                project.Methodology = model.Methodology?.Trim();
                project.SocioEconomicPartner = model.SocioEconomicPartner?.Trim();
                project.TargetSectors = model.TargetSectors?.Trim();
                project.ExpectedResults = model.ExpectedResults?.Trim();
                project.Impact = model.Impact?.Trim();
                project.ReferencesJson = referencesJson;

                // تحديث الصورة إذا تم رفع واحدة جديدة
                if (model.LogoFile != null && model.LogoFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "ProjectImages");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.LogoFile.CopyToAsync(stream);
                    }

                    project.LogoPath = "/uploads/ProjectImages/" + fileName;
                }

                project.LastActivity = DateTime.UtcNow;

                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}