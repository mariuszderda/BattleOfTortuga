namespace SharedData.Payloads
{
    [Serializable]
    public class ErrorPayload
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public ErrorPayload()
        { }

        public ErrorPayload(string errorMessage)
        {
            _errorMessage = errorMessage;
        }
    }
}