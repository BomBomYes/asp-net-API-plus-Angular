using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Models;
using SimpleBlog.Services;

namespace SimpleBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public PostController(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<IEnumerable<PostDto>>> FindPostByTitle(string search)
    {
        var posts = await _postService.FindPostByTitle(search);
        return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        var post = await _postService.GetPostAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PostDto>(post));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(PostDto newPostDto)
    {
        Post createdPost = await _postService.CreatePostAsync(newPostDto);

        return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, _mapper.Map<PostDto>(createdPost));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, PostDto updatedPostDto)
    {
        Post updatedPost = _mapper.Map<Post>(updatedPostDto);
        try
        {
            await _postService.UpdatePostAsync(id, updatedPost);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _postService.DeletePostAsync(id);
        return NoContent();
    }
}