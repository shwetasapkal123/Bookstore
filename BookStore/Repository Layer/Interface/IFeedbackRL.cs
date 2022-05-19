using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId);
        public string UpdateFeedback(FeedbackModel feedback, int userId);
        public bool DeleteFeedback(int feedbackId, int userId);
        public List<GetFeedBackModel> GetRecordsByBookId(int bookId);
    }
}
