﻿namespace FunTrophy.Shared.Model
{
    public class RaceDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsEnded { get; set; }
    }
}