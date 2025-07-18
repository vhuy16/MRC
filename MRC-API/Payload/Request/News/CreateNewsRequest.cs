﻿using Repository.Enum;

namespace MRC_API.Payload.Request.News
{
    public class CreateNewsRequest
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public TypeNewsEnum Type { get; set; }
    }
}
