namespace IMobi.School.Config
{
    public class IMobiException : Exception
    {
        public IMobiException(string devMessage = "", string displayMessage = "")
        {

        }
        public IMobiException(string devMessage = "", string displayMessage = "", Exception ex = null)
        {

        }
    }
}