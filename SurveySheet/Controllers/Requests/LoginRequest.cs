﻿using SurveySheet.Enums;

namespace SurveySheet.Controllers.Requests
{
    public class LoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
