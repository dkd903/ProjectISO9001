using System.Collections.Generic;

namespace SuperSchnell.Project.Mvc.Repositories
{
    public class SaveResult
    {
        public SaveResult(long assignedId, IEnumerable<string> messages, bool success)
        {
            AssignedId = assignedId;
            Messages = messages;
            Success = success;
        }

        public long AssignedId { get; private set; }
        public IEnumerable<string> Messages { get; private set; }
        public bool Success { get; private set; }
        public static SaveResult ErrorResult(IEnumerable<string> errors)
        {
            return new SaveResult(-1, errors, false);
        }
        public static SaveResult SuccessResult(long assignedId)
        {
            return new SaveResult(assignedId,new string[0],true);
        }
        public static SaveResult SuccessResult()
        {
            return new SaveResult(-1,new string[0],true);
        }
        public static SaveResult NotFoundResult()
        {
            return new SaveResult(-1, new[] { "SuperSchnell_Project_Mvc_Utilities_SaveResult_NotFoundError" }, false);
        }
        public static SaveResult ConcurrencyConflictResult()
        {
            return new SaveResult(-1, new[] { "SuperSchnell_Project_Mvc_Utilities_SaveResult_ConcurrencyError" }, false);
        }
    }
}