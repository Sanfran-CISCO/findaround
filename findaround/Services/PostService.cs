﻿using System;
using findaround.Utilities;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using MonkeyCache.FileStore;
using Newtonsoft.Json;

namespace findaround.Services
{
	public class PostService : IPostService
	{
        readonly HttpClient _client;
        readonly Uri _baseUrl = new Uri(Barrel.Current.Get<string>("BaseURL"));

        public PostService()
		{
            _client = BackendUtilities.ProduceHttpClient();
            _client.BaseAddress = _baseUrl;
        }

        public async Task<bool> AddPost(Post post)
        {
            _client.SetAuthenticationToken();

            var content = this.GetRequestContent(post);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/posts", content);
            }
            catch (HttpRequestException e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<bool> AddPostComment(Comment comment)
        {
            _client.SetAuthenticationToken();

            var content = this.GetRequestContent(comment);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/posts/comments", content);
            }
            catch (HttpRequestException e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<bool> DeletePost(int postId)
        {
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.DeleteAsync($"api/v1/findaround/posts/{postId}");
            }
            catch (HttpRequestException e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<bool> DeletePostComment(int commentId)
        {
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.DeleteAsync($"api/v1/findaround/posts/comments/{commentId}");
            }
            catch (HttpRequestException e)
            {
                return false;
            }

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<Post> GetPost(int postId)
        {
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.GetAsync($"api/v1/findaround/posts/get/{postId}");
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<Post>(responseContent);

            return post;
        }

        public async Task<List<Comment>> GetPostComments(int postId)
        {
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.GetAsync($"api/v1/findaround/posts/comments/{postId}");
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var comments = JsonConvert.DeserializeObject<List<Comment>>(responseContent);

            return comments;
        }

        public async Task<List<Post>> GetUserPosts(int userId)
        {
            _client.SetAuthenticationToken();

            var response = new HttpResponseMessage();

            try
            {
                await _client.GetAsync($"api/v1/findaround/posts");
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<Post>>(responseContent);

            return posts;
        }

        public async Task<List<Post>> MatchPosts(PostMatchingDto dto)
        {
            _client.SetAuthenticationToken();

            var content = this.GetRequestContent(dto);
            var response = new HttpResponseMessage();

            try
            {
                response = await _client.PostAsync("api/v1/findaround/posts/match", content);
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<Post>>(responseContent);

            return posts;
        }
    }
}
