﻿using imperugo.aspnet.core.training.Models;
using imperugo.aspnet.core.training.Repositories.Abstracts;
using imperugo.aspnet.core.training.Repositories.Responses;
using Microsoft.AspNetCore.Mvc;

namespace imperugo.aspnet.core.training.Apis
{
	// Good article about routing with aspnet core
	// http://stephenwalther.com/archive/2015/02/07/asp-net-5-deep-dive-routing

	[Route("api/[controller]")]
	public class BlogPostController : Controller
	{
		private readonly IBlogPostRepository blogPostRepository;

		public BlogPostController(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
		}

		// http://localhost:5000/api/BlogPost/GetPosts/?pageIndex=0&pageSize=10
		[HttpGet("GetPosts")]
		public PagedResult<BlogPost> GetPosts([FromQuery] int pageIndex, [FromQuery] int pageSize)
		{
			return this.blogPostRepository.GetPosts(pageIndex, pageSize);
		}

		// http://localhost:5000/api/BlogPost/GetBySlug/something
		[HttpGet("GetBySlug/{slug}")]
		public IActionResult ThisIsAWrongActionName(string slug)
		{
			if (string.IsNullOrEmpty(slug))
			{
				return BadRequest("Unable to read the specificed slug");
			}

			return Json(this.blogPostRepository.GetById(slug));
		}
	}
}