﻿using Buisness_Layer.Interface;
using Database_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }
        [Authorize]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel feedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.feedbackBL.AddFeedback(feedback, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback Added For this Book Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Enter valid BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("UpdateFeedback")]
        public IActionResult UpdateFeedback(FeedbackModel feedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.feedbackBL.UpdateFeedback(feedback, userId);
                if (result.Equals("Feedback Updated For this Book Successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("DeleteFeedback")]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.feedbackBL.DeleteFeedback(feedbackId, userId))
                {
                    return this.Ok(new { Status = true, Message = "Deleted From Feedback" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("GetAllFeedback")]
        public IActionResult GetAllFeedbackRecords(int bookId)
        {
            try
            {
                var result = this.feedbackBL.GetRecordsByBookId(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback All Records Fetched Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Enter valid BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
