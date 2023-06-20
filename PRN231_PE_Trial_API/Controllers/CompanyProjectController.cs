using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRN231_PE_Trial_API.Utils;
using PRN231_PE_Trial_API.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace PRN231_PE_Trial_API.Controllers
{
    [Authorize(Roles = "ProjectManager")]
    public class CompanyProjectController : ODataController
    {
        private readonly CompanyProjectRepository _companyProjectRepository;
        private readonly ParticipatingProjectRepository _participatingProjectRepository;

        public CompanyProjectController(CompanyProjectRepository companyProjectRepository, ParticipatingProjectRepository participatingProjectRepository)
        {
            _companyProjectRepository = companyProjectRepository;
            _participatingProjectRepository = participatingProjectRepository;
        }

        [EnableQuery(MaxExpansionDepth = 3)]
        public ActionResult<IQueryable<CompanyProject>> Get()
        {
            #region Manual map from CompanyProject to CompanyProjectViewModel (Disabled)
            //// Manual map from CompanyProject to CompanyProjectViewModel
            //var companyProject = _companyProjectRepository.GetCompanyProjects();
            //var companyProjectViewModelList = new List<CompanyProjectViewModel>();

            //foreach (var cp in companyProject)
            //{
            //    CompanyProjectViewModel companyProjectViewModel = new CompanyProjectViewModel()
            //    {
            //        CompanyProjectID = cp.CompanyProjectID,
            //        ProjectName = cp.ProjectName,
            //        ProjectDescription = cp.ProjectDescription,
            //        EstimatedStartDate = cp.EstimatedStartDate,
            //        EstimatedEndDate = cp.EstimatedEndDate
            //    };

            //    // Manual map from ParticipatingProject to ParticipatingProjectViewModel
            //    List<ParticipatingProjectViewModel> participatingProjectViewModels = new List<ParticipatingProjectViewModel>();
            //    foreach (var pp in cp.ParticipatingProjects)
            //    {
            //        ParticipatingProjectViewModel ppViewModel = new ParticipatingProjectViewModel()
            //        {
            //            EmployeeID = pp.EmployeeID,
            //            CompanyProjectID = pp.CompanyProjectID,
            //            StartDate = pp.StartDate,
            //            EndDate = pp.EndDate
            //        };
            //        participatingProjectViewModels.Add(ppViewModel);
            //    }
            //    companyProjectViewModel.ParticipatingProjects = participatingProjectViewModels;


            //    companyProjectViewModelList.Add(companyProjectViewModel);
            //}

            #endregion


            //companyProjectViewModelList (this is ViewModel Return) (goes with ManualMap and change in program.cs)
            //_companyProjectRepository.GetCompanyProjects() (this is Entity Return)
            return Ok(_companyProjectRepository.GetCompanyProjects());
        }

        #region Search by ProjectName (Disabled) (Answer inside)
        //[EnableQuery]
        //public ActionResult<CompanyProject> Get([FromRoute] string key)
        //{
        //    // Note: odata can take string as a key, but only if it was defined as key in the model
        //    // Good answer: https://stackoverflow.com/questions/36482317/change-odata-method-key-from-integer-to-string 
        //    // Odata search key base on it's model as set in program.cs (EDMModel)

        //    #region Manual map from CompanyProject to CompanyProjectViewModel (Disabled)
        //    // Manual map from CompanyProject to CompanyProjectViewModel
        //    //CompanyProject companyProject = _companyProjectRepository.GetCompanyProject(x => x.ProjectName.Contains(key));
        //    //CompanyProjectViewModel companyProjectViewModel = new CompanyProjectViewModel()
        //    //{
        //    //    CompanyProjectID = companyProject.CompanyProjectID,
        //    //    ProjectName = companyProject.ProjectName,
        //    //    ProjectDescription = companyProject.ProjectDescription,
        //    //    EstimatedStartDate = companyProject.EstimatedStartDate,
        //    //    EstimatedEndDate = companyProject.EstimatedEndDate
        //    //};

        //    //// Manual map from ParticipatingProject to ParticipatingProjectViewModel
        //    //List<ParticipatingProjectViewModel> participatingProjectViewModels = new List<ParticipatingProjectViewModel>();
        //    //foreach(var pp in companyProject.ParticipatingProjects)
        //    //{
        //    //    ParticipatingProjectViewModel ppViewModel = new ParticipatingProjectViewModel()
        //    //    {
        //    //        EmployeeID = pp.EmployeeID,
        //    //        CompanyProjectID = pp.CompanyProjectID,
        //    //        StartDate = pp.StartDate,
        //    //        EndDate = pp.EndDate
        //    //    };
        //    //    participatingProjectViewModels.Add(ppViewModel);
        //    //}

        //    //companyProjectViewModel.ParticipatingProjects = participatingProjectViewModels;
        //    #endregion

        //    //companyProjectViewModel (this is ViewModel Return) (goes with ManualMap and change in program.cs)
        //    //_companyProjectRepository.GetCompanyProject(x => x.ProjectName.Contains(key)) (this is Entity Return)
        //    return Ok(_companyProjectRepository.GetCompanyProject(x => x.ProjectName.Contains(key)));
        //}
        #endregion

        [HttpGet]
        [Route("api/CompanyProject/SearchByProjectName")]
        public ActionResult<List<CompanyProjectViewModel>> SearchByProjectName(string target)
        {
            var companyProject = _companyProjectRepository.GetCompanyProjectsByProjectName(target);

            if (companyProject == null)
            {
                return NotFound();
            }

            #region manual map from CompanyProject to CompanyProjectViewModel
            var cpViewModelList = new List<CompanyProjectViewModel>();

            foreach (var cp in companyProject)
            {
                CompanyProjectViewModel companyProjectViewModel = new CompanyProjectViewModel()
                {
                    CompanyProjectID = cp.CompanyProjectID,
                    ProjectName = cp.ProjectName,
                    ProjectDescription = cp.ProjectDescription,
                    EstimatedStartDate = cp.EstimatedStartDate,
                    EstimatedEndDate = cp.EstimatedEndDate,
                    ParticipatingProjects = cp.ParticipatingProjects.Select(x => new ParticipatingProjectViewModel()
                    {
                        EmployeeID = x.EmployeeID,
                        CompanyProjectID = x.CompanyProjectID,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Employee = x.Employee == null ? null : new EmployeeViewModel()
                        {
                            EmployeeID = x.Employee.EmployeeID,
                            FullName = x.Employee.FullName,
                            Skills = x.Employee.Skills,
                            Telephone = x.Employee.Telephone,
                            Address = x.Employee.Address,
                            Status = x.Employee.Status,
                            DepartmentID = x.Employee.DepartmentID
                        }
                    }).ToList()
                };
                cpViewModelList.Add(companyProjectViewModel);
            }
            #endregion

            return Ok(cpViewModelList);
        }

        [HttpGet]
        [Route("api/CompanyProject/SearchByEstimateStartDate")]
        public ActionResult<List<CompanyProjectViewModel>> SearchByEstimateStartDate(DateTime targetDate)
        {
            var companyProject = _companyProjectRepository.GetCompanyProjectsByEstimatedStartDate(targetDate);

            if (companyProject == null)
            {
                return NotFound();
            }

            #region manual map from CompanyProject to CompanyProjectViewModel
            var cpViewModelList = new List<CompanyProjectViewModel>();

            foreach (var cp in companyProject)
            {
                CompanyProjectViewModel companyProjectViewModel = new CompanyProjectViewModel()
                {
                    CompanyProjectID = cp.CompanyProjectID,
                    ProjectName = cp.ProjectName,
                    ProjectDescription = cp.ProjectDescription,
                    EstimatedStartDate = cp.EstimatedStartDate,
                    EstimatedEndDate = cp.EstimatedEndDate,
                    ParticipatingProjects = cp.ParticipatingProjects.Select(x => new ParticipatingProjectViewModel()
                    {
                        EmployeeID = x.EmployeeID,
                        CompanyProjectID = x.CompanyProjectID,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Employee = x.Employee == null ? null : new EmployeeViewModel()
                        {
                            EmployeeID = x.Employee.EmployeeID,
                            FullName = x.Employee.FullName,
                            Skills = x.Employee.Skills,
                            Telephone = x.Employee.Telephone,
                            Address = x.Employee.Address,
                            Status = x.Employee.Status,
                            DepartmentID = x.Employee.DepartmentID
                        }
                    }).ToList()
                };
                cpViewModelList.Add(companyProjectViewModel);
            }
            #endregion

            return Ok(cpViewModelList);
        }

        [EnableQuery]
        public ActionResult Post([FromBody] CompanyProjectViewModel companyProjectViewModel)
        {
            bool status = false;
            List<string> errorMessage = new List<string>();

            // Check if Company Project View Model is null
            if (companyProjectViewModel == null)
            {
                errorMessage.Add("Fail to get Company Project View Model from request");

                return new JsonResult(new
                {
                    status = status,
                    errorMessage = errorMessage
                });
            }

            #region Validate data
            // Validate data
            if (companyProjectViewModel.ProjectName == null || companyProjectViewModel.ProjectName == string.Empty)
            {
                errorMessage.Add("Project name is required");
            }
            else
            {
                string[] words = companyProjectViewModel.ProjectName.Split(' ');

                foreach (string word in words)
                {
                    if (!Regex.IsMatch(word, @"^[A-Z][a-zA-Z0-9/]*$"))
                    {
                        errorMessage.Add("Invalid project name format");
                        break;
                    }
                }
            }
            if (companyProjectViewModel.ProjectDescription == null || companyProjectViewModel.ProjectDescription == string.Empty)
            {
                errorMessage.Add("Project description is required");
            }
            if (companyProjectViewModel.EstimatedStartDate == null)
            {
                errorMessage.Add("Estimated start date is required");
            }
            if (companyProjectViewModel.EstimatedEndDate == null)
            {
                errorMessage.Add("Estimated end date is required");
            }
            if(companyProjectViewModel.EstimatedStartDate > companyProjectViewModel.EstimatedEndDate)
            {
                errorMessage.Add("Estimated start date must be before estimated end date");
            }
            if (companyProjectViewModel.ParticipatingProjects.Count == 0)
            {
                errorMessage.Add("At least one participating employee is required");
            }

            List<int> employeeIDList = new List<int>();

            // Validate participating projects
            foreach(var pp in companyProjectViewModel.ParticipatingProjects)
            {
                // Validate data
                if(pp.EmployeeID == null)
                {
                    errorMessage.Add("Employee ID is required");
                }
                if(pp.StartDate == null)
                {
                    errorMessage.Add($"Employee {pp.EmployeeID} Start date is required");
                }
                if(pp.EndDate == null)
                {
                    errorMessage.Add($"Employee {pp.EmployeeID} End date is required");
                }
                if(pp.StartDate > pp.EndDate)
                {
                    errorMessage.Add($"Employee {pp.EmployeeID} Start date must be before end date");
                }

                // Check Duplicate Employee ID
                if(employeeIDList.Contains((int)pp.EmployeeID))
                {
                    errorMessage.Add($"Duplicate employee ID {pp.EmployeeID}");
                }
                else
                {
                    employeeIDList.Add((int)pp.EmployeeID);
                }
            }
            #endregion


            // If data is valid, create new company project
            if (errorMessage.Count == 0)
            {
                #region Manual map from CompanyProjectViewModel to CompanyProject for Create
                var companyProject = new CompanyProject
                {
                    ProjectName = companyProjectViewModel.ProjectName,
                    ProjectDescription = companyProjectViewModel.ProjectDescription,
                    EstimatedStartDate = (DateTime)companyProjectViewModel.EstimatedStartDate,
                    EstimatedEndDate = (DateTime)companyProjectViewModel.EstimatedEndDate,
                    ParticipatingProjects = new List<ParticipatingProject>()
                };

                foreach(var pp in companyProjectViewModel.ParticipatingProjects)
                {
                    var participatingProject = new ParticipatingProject
                    {
                        EmployeeID = (int)pp.EmployeeID,
                        StartDate = (DateTime)pp.StartDate,
                        EndDate = (DateTime)pp.EndDate
                    };
                    companyProject.ParticipatingProjects.Add(participatingProject);
                }
                #endregion

                try
                {
                    _companyProjectRepository.Create(companyProject);
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    errorMessage.Add(ex.Message);
                }
            }

            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }

        /// <summary>
        /// Update Company Project and it's Participating Projects. Updating Employee or Department will result in error code 500
        /// </summary>
        /// <param name="key"></param>
        /// <param name="companyProjectViewModel"></param>
        /// <returns></returns>
        [EnableQuery]
        public ActionResult Patch([FromRoute] int key, [FromBody] CompanyProjectViewModel companyProjectViewModel)
        {
            bool status = false;
            List<string> errorMessage = new List<string>();

            #region Validate data
            // Validate data
            if (companyProjectViewModel.ProjectName == null || companyProjectViewModel.ProjectName == string.Empty)
            {
                errorMessage.Add("Project name is required");
            }
            else
            {
                string[] words = companyProjectViewModel.ProjectName.Split(' ');
                foreach (string word in words)
                {
                    if (!Regex.IsMatch(word, @"^[A-Z][a-zA-Z0-9/]*$"))
                    {
                        errorMessage.Add("Invalid project name format");
                        break;
                    }
                }
            }
            if (companyProjectViewModel.ProjectDescription == null || companyProjectViewModel.ProjectDescription == string.Empty)
            {
                errorMessage.Add("Project description is required");
            }
            if (companyProjectViewModel.EstimatedStartDate == null)
            {
                errorMessage.Add("Estimated start date is required");
            }
            if (companyProjectViewModel.EstimatedEndDate == null)
            {
                errorMessage.Add("Estimated end date is required");
            }
            if (companyProjectViewModel.EstimatedStartDate > companyProjectViewModel.EstimatedEndDate)
            {
                errorMessage.Add("Estimated start date must be before estimated end date");
            }
            if (companyProjectViewModel.ParticipatingProjects.Count == 0)
            {
                errorMessage.Add("At least one participating employee is required");
            }
            List<int> employeeIDList = new List<int>();
            // Validate participating projects
            foreach (var pp in companyProjectViewModel.ParticipatingProjects)
            {
                // Validate data
                if (pp.EmployeeID == null)
                {
                    errorMessage.Add("Employee ID is required");
                }
                if (pp.StartDate == null)
                {
                    errorMessage.Add($"Employee {pp.EmployeeID} Start date is required");
                }
                if (pp.EndDate == null)
                {
                    errorMessage.Add($"Employee {pp.EmployeeID} End date is required");
                }
                if (pp.StartDate > pp.EndDate)
                {
                    errorMessage.Add($"Employee {pp.EmployeeID} Start date must be before end date");
                }
                // Check Duplicate Employee ID
                if (employeeIDList.Contains((int)pp.EmployeeID))
                {
                    errorMessage.Add($"Duplicate employee ID {pp.EmployeeID}");
                }
                else
                {
                    employeeIDList.Add((int)pp.EmployeeID);
                }
            }
            #endregion

            #region Manual map from CompanyProjectViewModel to CompanyProject for Update Company Project Only
            var companyProject = new CompanyProject
            {
                CompanyProjectID = (int)companyProjectViewModel.CompanyProjectID,
                ProjectName = companyProjectViewModel.ProjectName,
                ProjectDescription = companyProjectViewModel.ProjectDescription,
                EstimatedStartDate = (DateTime)companyProjectViewModel.EstimatedStartDate,
                EstimatedEndDate = (DateTime)companyProjectViewModel.EstimatedEndDate
            };


            #endregion

            try
            {
                // Note: UpdateTrackedEntity and Update can't update the child entities (need to split it down)
                _companyProjectRepository.Update(companyProject);

                #region Update/Create/Delete Participating Projects
                // Get Participating Projects from database
                ParticipatingProjectComparer comparer = new ParticipatingProjectComparer();
                var oldParticipatingProjects = _participatingProjectRepository.GetParticipatingProjectsByCompanyProjectID((int)companyProjectViewModel.CompanyProjectID);
                var newParticipatingProjects = new List<ParticipatingProject>();
                #region Map Participating Projects
                foreach (var pp in companyProjectViewModel.ParticipatingProjects)
                {
                    var participatingProject = new ParticipatingProject
                    {
                        CompanyProjectID = (int)companyProjectViewModel.CompanyProjectID,
                        EmployeeID = (int)pp.EmployeeID,
                        StartDate = (DateTime)pp.StartDate,
                        EndDate = (DateTime)pp.EndDate
                    };
                    newParticipatingProjects.Add(participatingProject);
                }
                #endregion

                var excludeParticipatingProject = oldParticipatingProjects.Except(newParticipatingProjects, comparer);

                // Update + Create
                foreach (var pp in newParticipatingProjects)
                {
                    if (oldParticipatingProjects.Contains(pp, comparer))
                    {
                        // Update
                        var oldPP = oldParticipatingProjects.Where(x => x.EmployeeID == pp.EmployeeID).FirstOrDefault();
                        oldPP.StartDate = pp.StartDate;
                        oldPP.EndDate = pp.EndDate;
                        _participatingProjectRepository.Update(oldPP);
                    }
                    else
                    {
                        // Create
                        _participatingProjectRepository.Create(pp);
                    }
                }

                // Delete
                foreach (var pp in excludeParticipatingProject)
                {
                    _participatingProjectRepository.Delete(x => x.CompanyProjectID == pp.CompanyProjectID && x.EmployeeID == pp.EmployeeID);
                }


                #endregion

                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage.Add(ex.Message);
            }

            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage
            });

        }

        [EnableQuery]
        public ActionResult Delete([FromRoute] int key)
        {
            bool status = false;
            List<string> errorMessage = new List<string>();
            try
            {
                _companyProjectRepository.Delete(id => id.CompanyProjectID == key);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage.Add(ex.Message);
            }
            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }

    }
}
