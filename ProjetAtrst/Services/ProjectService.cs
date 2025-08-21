using Microsoft.EntityFrameworkCore;
using ProjetAtrst.DTO;
using ProjetAtrst.Helpers;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.Repositories;
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
        public async Task CreateProjectAsync(ProjectCreateViewModel model, string researcherId)
        {
            //// حفظ الشعار إذا تم تحميله
            //string? logoPath = null;
            //if (model.LogoFile != null && model.LogoFile.Length > 0)
            //{
            //    var uploadsFolder = Path.Combine("wwwroot", "uploads", "logos");
            //    if (!Directory.Exists(uploadsFolder))
            //        Directory.CreateDirectory(uploadsFolder);

            //    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);
            //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await model.LogoFile.CopyToAsync(fileStream);
            //    }

            //    logoPath = Path.Combine("uploads", "logos", uniqueFileName).Replace("\\", "/");
            //}

            // تحويل المراجع إلى JSON
            string referencesJson = JsonSerializer.Serialize(model.References ?? new List<string>());

            // إنشاء المشروع
            var project = new Project
            {
                Title = model.Title,
                
               // LogoPath = logoPath,
                
                IsAcceptingJoinRequests = model.IsAcceptingJoinRequests,

                Keywords = model.Keywords,
                Domain = model.Domain,
                Axis = model.Axis,
                Theme = model.Theme,
                Nature = model.Nature,
                TRL = model.TRL,
                PNR = model.PNR,
                DurationInMonths = model.DurationInMonths,
                HostInstitution = model.HostInstitution,

                CurrentState = model.CurrentState,
                Motivation = model.Motivation,
                Methodology = model.Methodology,

                SocioEconomicPartner = model.SocioEconomicPartner,
                ExpectedResults = model.ExpectedResults,
                TargetSectors = model.TargetSectors,
                Impact = model.Impact,

                ReferencesJson = referencesJson,

                CreationDate = DateTime.UtcNow,
                LastActivity = DateTime.UtcNow
            };

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveAsync();

            var membership = new ProjectMembership
            {
                ProjectId = project.Id, 
                ResearcherId = researcherId,
                Role = Role.Leader,
                JoinedAt = DateTime.UtcNow
            };

            await _unitOfWork.ProjectMemberships.AddAsync(membership);
            await _unitOfWork.SaveAsync();
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

       
        public async Task<ProjectEditViewModel?> GetProjectForEditAsync(string researcherId, int projectId)
        {
            var membership = await _unitOfWork.ProjectMemberships
                .GetByResearcherAndProjectAsync(researcherId, projectId);

            if (membership == null || membership.Role != Role.Leader)
                return null;

            var project = membership.Project;

            return new ProjectEditViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
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
            project.Title = model.Title;
            project.Description = model.Description;
            project.LastActivity = DateTime.UtcNow;
            project.LogoPath = model.CurrentLogoPath; // احفظ المسار القديم إذا كان موجودًا

            // ✅ معالجة الصورة إذا تم رفعها
            if (model.LogoFile != null && model.LogoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "ProjectImages");
                Directory.CreateDirectory(uploadsFolder); // تأكد أن المجلد موجود أو يتم إنشاؤه

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(stream);
                }

                // احفظ المسار النسبي الذي يمكن استخدامه في العرض
                project.LogoPath = "/uploads/ProjectImages/" + fileName;
            }

            await _unitOfWork.SaveAsync();
            return true;
        }

        //delete
        //public async Task<(List<AvailableProjectViewModel> Projects, int TotalCount)> GetAvailableProjectsAsync(string researcherId, int pageNumber, int pageSize)
        //{
        //    var (projects, totalCount) = await _unitOfWork.Projects.GetAvailableProjectsForJoinAsync(researcherId, pageNumber, pageSize);

        //    var result = projects.Select(p =>
        //    {
        //        var leaderMembership = p.ProjectMemberships.FirstOrDefault(pm => pm.Role == Role.Leader);

        //        return new AvailableProjectViewModel
        //        {
        //            Id = p.Id,
        //            Title = p.Title,
        //            Description = p.Description,
        //            CreationDate = p.CreationDate,
        //            ImageUrl = p.LogoPath,
        //            LeaderId = leaderMembership?.ResearcherId ?? "غير معروف",
        //            LeaderFullName = leaderMembership?.Researcher?.User?.FullName ?? "غير معروف"
        //        };
        //    }).ToList();

        //    return (result, totalCount);
        //}

        //------New-------//
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

        private static IQueryable<AvailableProjectDto> ApplySearch(
            IQueryable<AvailableProjectDto> query, string? search)
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
                Name = m.Researcher.User.FullName,
                Email = m.Researcher.User.Email,
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
                SenderName = r.Sender.User.FullName,
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
                SenderName = r.Receiver.User.FullName,
                Status = r.Status,
                SentAt = r.CreatedAt
            }).ToList();
        }

    }
}