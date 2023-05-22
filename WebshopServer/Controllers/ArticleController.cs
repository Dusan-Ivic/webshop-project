﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Exceptions;
using WebshopServer.Interfaces;

namespace WebshopServer.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult GetAllArticles()
        {
            return Ok(_articleService.GetAllArticles());
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleById(long id)
        {
            ArticleDto article;

            try
            {
                article = _articleService.GetArticleById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(article);
        }

        [HttpPost]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult CreateArticle([FromBody] ArticleDto articleDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            ArticleDto article;

            try
            {
                article = _articleService.CreateArticle(articleDto, userId);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(article);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult UpdateArticle(long id, [FromBody] ArticleDto articleDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            ArticleDto article;

            try
            {
                article = _articleService.UpdateArticle(id, articleDto, userId);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }
            catch (ForbiddenActionException)
            {
                return Forbid();
            }

            return Ok(article);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult DeleteArticle(long id)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            try
            {
                _articleService.DeleteArticle(id, userId);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ForbiddenActionException)
            {
                return Forbid();
            }

            return Ok();
        }
    }
}
