﻿namespace Comp584TaskApi.DTO
{
    public class TaskRequest
    {
        public string Body { get; set; } = null!;
        public int? CategoryId { get; set; }
    }
}
