namespace Rune
{
    public class Result
    {
        public bool success;
        public string reason;

        public static Result Ok() => new() { success = true };
        public static Result Fail(string reason = "") => new() { success = false, reason = reason };
    }
}