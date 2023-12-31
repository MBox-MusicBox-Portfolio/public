﻿namespace MBox.Models.Presenters
{
    public class ResponsePresenter<T>
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }
        public int Status { get; set; }

        private ResponsePresenter()
        {
            IsSuccess = false;
        }
    }

    public class ResponsePresenter
    {
        public bool Success { get; set; }
        public List<object> Value { get; set; }
        public List<object> Errors { get; set; }
        public int Status { get; set; }
        public ResponsePresenter(List<object> value, List<object> errors, bool success = true)
        {
            Success = success;
            Value = value;
            Errors = errors;
        }
        public ResponsePresenter()
        {
            Value = new();
            Errors = new();
        }
    }
}
