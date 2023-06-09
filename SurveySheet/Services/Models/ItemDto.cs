﻿using SurveySheet.Repositories.Models;

namespace SurveySheet.Services.Models
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsChecked { get; set; } = false;

        public ItemDto()
        {

        }

        public ItemDto(Item item)
        {
            Id = item.Id;
            Title = item.Title;
        }
    }
}
