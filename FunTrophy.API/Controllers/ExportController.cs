﻿using FunTrophy.API.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FunTrophy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportController : AnonymousControllerBase
    {
        private readonly ILogger<ExportController> _logger;
        private readonly IExportService _exportService;

        public ExportController(ILogger<ExportController> logger, IExportService exportService)
        {
            _logger = logger;
            _exportService = exportService;
        }

        /// <summary>
        /// Generate an excel file with a page for each time ajdustment category
        /// </summary>
        /// <param name="raceId">Race id</param>
        /// <returns>Excel file</returns>
        [HttpGet("categories")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTeamByTimeAdjustmentCategoriesFile(int raceId)
        {
            if (raceId <= 0)
                return BadRequest("RaceId must be positive");

            var fileBytes = await _exportService.GetTeamsByTimeAdjustmentCategoryFile(raceId);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FunTrophy_Catégories.xlsx");
        }

        /// <summary>
        /// Generate an excel file with a page for each color with each track
        /// </summary>
        /// <param name="raceId">Race id</param>
        /// <returns>Excel file</returns>
        [HttpGet("colors")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTracksByColorFile(int raceId)
        {
            if (raceId <= 0)
                return BadRequest("RaceId must be positive");

            var fileBytes = await _exportService.GetTracksByColorFile(raceId);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FunTrophy_Couleurs.xlsx");
        }

        /// <summary>
        /// Generate an excel file with a page for each color with each track
        /// </summary>
        /// <param name="raceId">Race id</param>
        /// <returns>Excel file</returns>
        [HttpGet("categoriesByColor")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTimeAdjustmentCategoriesByColor(int raceId)
        {
            if (raceId <= 0)
                return BadRequest("RaceId must be positive");

            var fileBytes = await _exportService.GetTimeAdjustmentCategoriesByColor(raceId);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FunTrophy_Bonus.xlsx");
        }
    }
}