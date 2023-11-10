namespace ProjetoBlogApi.ViewModel
{
    public class ResultViewModel<T>
    {
        /// <summary>
        /// Retorna uma visualização para o erro 500 por exemplo.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errors"></param>
        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }

        public T ? Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
    }
}