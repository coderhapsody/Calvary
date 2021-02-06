using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.Member;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using System.Collections.Generic;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Master.Controllers
{
	public class MemberController : BaseController
	{
        private readonly HobbyProvider hobbyProvider;
        private readonly SkillProvider skillProvider;
        private readonly MemberProvider memberProvider;
        private readonly EthnicProvider ethnicProvider;
        private readonly RegionProvider regionProvider;
        private readonly ShrineProvider shrineProvider;
        private readonly JobProvider jobProvider;
	    private readonly EducationGradeProvider educationGradeProvider;
        private readonly EducationMajorProvider educationMajorProvider;        

        public MemberController(
            MemberProvider memberProvider, 
            EthnicProvider ethnicProvider, 
            RegionProvider regionProvider, 
            ShrineProvider shrineProvider,
            JobProvider jobProvider,
            EducationGradeProvider educationGradeProvider,
            EducationMajorProvider educationMajorProvider,
            HobbyProvider hobbyProvider,
            SkillProvider skillProvider)            
        {			
            this.memberProvider = memberProvider;
            this.ethnicProvider = ethnicProvider;
            this.regionProvider = regionProvider;
            this.shrineProvider = shrineProvider;
            this.jobProvider = jobProvider;
            this.educationGradeProvider = educationGradeProvider;
            this.educationMajorProvider = educationMajorProvider;
            this.hobbyProvider = hobbyProvider;
            this.skillProvider = skillProvider;
        }
		
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult IsMemberNoValid(int memberId, string memberNo)
        {
            var ajaxViewModel = new AjaxViewModel(memberProvider.IsMemberNoValid(memberId, memberNo), null, null);
            return Json(ajaxViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            return View("CreateEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var model = new CreateEditViewModel();
            var member = memberProvider.GetMember(id);
            Mapper.DynamicMap(member, model);

            if(!System.IO.File.Exists("~/Photo/" + model.Photo))
                model.Photo = "anonymous.jpg";

            return View("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            Member member = memberProvider.GetMember(model.Id);
            Mapper.DynamicMap(model, member);
            ValidateData(member);

            memberProvider.UpdateMember(member);

            //var jsonViewModel = new AjaxViewModel(true, model, null);
            return RedirectToAction("Index");
        }

        private void ValidateData(Member member)
        {
            if (String.IsNullOrEmpty(member.BloodType))
                member.Rhesus = null;
            else
            {
                if (String.IsNullOrEmpty(member.Rhesus))
                    member.Rhesus = "+";
            }
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var member = new Member();
            Mapper.DynamicMap(model, member);

            ValidateData(member);

            memberProvider.AddMember(member);

            return RedirectToAction("Index");
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request, bool includeResigned, bool includeDeceased)
        {
            if (!request.Sorts.Any())
                request.Sorts.Add(new Kendo.Mvc.SortDescriptor("ChangedWhen", System.ComponentModel.ListSortDirection.Descending));

            var list = memberProvider.List(includeResigned, includeDeceased);
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(memberProvider.DeleteMember);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }            
        }

        public ActionResult EditPhoto()
        {
            return View();
        }

       

        #region Look up to other master data
        public ActionResult GetMaritalStatuses()
	    {
	        return Json(Constants.MaritalStatuses, JsonRequestBehavior.AllowGet);
	    }

        public ActionResult GetBloodTypes()
        {
            return Json(Constants.BloodTypes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEthnics()
        {
            var ethnics = ethnicProvider.GetEthnics();
            return Json(ethnics, JsonRequestBehavior.AllowGet);
        }

	    public ActionResult GetRegions()
	    {
	        var regions = regionProvider.GetLabeledRegions();
            return Json(regions, JsonRequestBehavior.AllowGet);
	    }

        public ActionResult GetGenders()
        {
            var genders = Constants.Genders;
            return Json(genders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShrines()
        {
            var shrines = shrineProvider.GetShrines();
            return Json(shrines, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChrismationTypes()
        {
            var chrismationTypes = Constants.ChrismationTypes;
            return Json(chrismationTypes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetJobs()
        {
            var jobs = jobProvider.GetJobs();
            return Json(jobs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEducationMajors()
        {
            var eduMajors = educationMajorProvider.GetEducationMajors();
            return Json(eduMajors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEducationGrades()
        {
            var eduGrades = educationGradeProvider.GetEducationGrades();
            return Json(eduGrades, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHobbies()
        {
            var hobbies = hobbyProvider.GetHobbies();
            return Json(hobbies, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSkills()
        {
            var skills = skillProvider.GetSkills();
            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBaptizedReasons()
        {
            var baptizedReasons = memberProvider.GetBaptizedReasons();
            return Json(baptizedReasons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetResignReasons()
        {
            var resignReasons = memberProvider.GetResignReasons();
            return Json(resignReasons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListVisits([DataSourceRequest] DataSourceRequest request, int memberId)
        {
            var visits = memberProvider.ListMemberVisit(memberId);
            return Json(visits.ToDataSourceResult(request));
        }
        #endregion

        #region Marriages
        public ActionResult ListMarriages([DataSourceRequest] DataSourceRequest request, int memberId)
        {
            var marriages = memberProvider.ListMarriage(memberId);
            return Json(marriages.ToDataSourceResult(request));
        }

        public ActionResult CreateMarriage(int memberId)
        {            
            var model = new MemberMarriageCreateEditViewModel();
            model.MarriageDate = DateTime.Today;
            model.MemberId = memberId;
            return PartialView("MarriageCreateEdit", model);
        }

        [HttpPost]
        public ActionResult CreateMarriage(FormCollection form, MemberMarriageCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var memberMarriage = new MemberMarriage();
                    Mapper.DynamicMap(model, memberMarriage);
                    memberMarriage.SpouseId = model.MarriedToMember ? model.SpouseId : (int?)null;
                    memberProvider.AddMarriage(memberMarriage);
                    return Json(new AjaxViewModel(true, model, null));
                }
                catch (Exception ex)
                {
                    return Json(new AjaxViewModel(false, null, ex.Message));
                }

            }
            return View(model);
        }

        public ActionResult EditMarriage(int memberMarriageId)
        {
            var model = new MemberMarriageCreateEditViewModel();
            var marriageInfo = memberProvider.GetMarriage(memberMarriageId);
            Mapper.DynamicMap(marriageInfo, model);
            model.MarriedToMember = marriageInfo.SpouseId.HasValue;            
            model.SpouseNo = marriageInfo?.Member?.MemberNo;
            return PartialView("MarriageCreateEdit", model);
        }

        [HttpPost]
        public ActionResult EditMarriage(FormCollection form, MemberMarriageCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var memberMarriage = new MemberMarriage();

                    model.MarriedToMember = model.SpouseId != 0;

                    Mapper.DynamicMap(model, memberMarriage);
                    memberMarriage.SpouseId = model.MarriedToMember ? model.SpouseId : (int?)null;
                    memberProvider.UpdateMarriage(memberMarriage);
                    return Json(new AjaxViewModel(true, model, null));
                }
                catch (Exception ex)
                {
                    return Json(new AjaxViewModel(false, null, ex.Message));
                }

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteMarriage(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(memberProvider.DeleteMarriage);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
        #endregion

        #region MemberStateHistory (Status DKH)
        public ActionResult ListMemberStateHistory([DataSourceRequest] DataSourceRequest request, int memberId)
        {
            var memberStateHistory = memberProvider.ListMemberStateHistory(memberId);
            return Json(memberStateHistory.ToDataSourceResult(request));
        }

        public ActionResult CreateMemberStateHistory(int memberId)
        {
            var model = new MemberStateHistoryCreateEditViewModel();
            model.MemberId = memberId;
            model.EffDate = DateTime.Today;
            return PartialView("MemberStateHistoryCreateEdit", model);
        }

        [HttpPost]
        public ActionResult CreateMemberStateHistory(FormCollection form, ListMemberStateHistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var memberStateHist = new MemberStateHistory();
                    Mapper.DynamicMap(model, memberStateHist);
                    memberProvider.AddMemberStateHistory(memberStateHist);
                    return Json(new AjaxViewModel(true, model, null));
                }
                catch (Exception ex)
                {
                    return Json(new AjaxViewModel(false, null, ex.Message));
                }

            }
            return View();
        }

        public ActionResult EditMemberStateHistory(int memberStateHistoryId)
        {
            var model = new MemberStateHistoryCreateEditViewModel();
            var memberStateHistory = memberProvider.GetMemberStateHistory(memberStateHistoryId);
            Mapper.DynamicMap(memberStateHistory, model);
            return PartialView("MemberStateHistoryCreateEdit", model);
        }

        [HttpPost]
        public ActionResult EditMemberStateHistory(FormCollection form, MemberStateHistoryCreateEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var memberStateHist = new MemberStateHistory();
                    Mapper.DynamicMap(model, memberStateHist);
                    memberProvider.UpdateMemberStateHistory(memberStateHist);
                    return Json(new AjaxViewModel(true, model, null));
                }
                catch(Exception ex)
                {
                    return Json(new AjaxViewModel(false, null, ex.Message));
                }

            }
            return View(model);
        }

        public ActionResult GetMemberStates()
        {
            var result = memberProvider.GetMemberStates();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMemberStateHistory(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(memberProvider.DeleteMemberStateHistory);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
        #endregion

        #region Notes
        public ActionResult ListNotes([DataSourceRequest] DataSourceRequest request, int memberId)
        {
            var list = memberProvider.ListNotes(memberId);
            return Json(list.ToDataSourceResult(request));
        }

        public ActionResult CreateMemberNotes(int memberId)
        {
            var model = new MemberNoteViewModel();
            model.MemberId = memberId;
            return PartialView("NotesCreateEdit", model);
        }

        [HttpPost]
        public ActionResult CreateMemberNotes(FormCollection form, ListMemberStateHistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var memberNote = new MemberNote();
                    Mapper.DynamicMap(model, memberNote);
                    memberProvider.AddMemberNote(memberNote);
                    return Json(new AjaxViewModel(true, model, null));
                }
                catch (Exception ex)
                {
                    return Json(new AjaxViewModel(false, null, ex.Message));
                }

            }
            return View();
        }

        public ActionResult EditMemberNotes(int memberNoteId)
        {
            var model = new MemberNoteViewModel();
            var memberNote = memberProvider.GetMemberNote(memberNoteId);
            Mapper.DynamicMap(memberNote, model);
            return PartialView("NotesCreateEdit", model);
        }

        [HttpPost]
        public ActionResult EditMemberNotes(FormCollection form, MemberNoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var memberNote = new MemberNote();
                    Mapper.DynamicMap(model, memberNote);
                    memberProvider.UpdateMemberNote(memberNote);
                    return Json(new AjaxViewModel(true, model, null));
                }
                catch (Exception ex)
                {
                    return Json(new AjaxViewModel(false, null, ex.Message));
                }

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteMemberNotes(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(memberProvider.DeleteMemberNote);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
        #endregion

    }
}
			





