﻿using Buisness_Layer.Interface;
using Database_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Services
{
    public class FeedbackBL:IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedback, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UpdateFeedback(FeedbackModel feedback, int userId)
        {
            try
            {
                return this.feedbackRL.UpdateFeedback(feedback, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteFeedback(int feedbackId, int userId)
        {
            try
            {
                return this.feedbackRL.DeleteFeedback(feedbackId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetFeedBackModel> GetRecordsByBookId(int bookId)
        {
            try
            {
                return this.feedbackRL.GetRecordsByBookId(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
