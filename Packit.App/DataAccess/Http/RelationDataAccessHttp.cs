﻿// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 04-13-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="RelationDataAccessHttp.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Packit.App.Services;
using Packit.Exceptions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    /// <summary>
    /// Class RelationDataAccessHttp.
    /// Implements the <see cref="Packit.App.DataAccess.IRelationDataAccess{T1, T2}" />
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// <typeparam name="T2">The type of the t2.</typeparam>
    /// <seealso cref="Packit.App.DataAccess.IRelationDataAccess{T1, T2}" />
    public sealed class RelationDataAccessHttp<T1, T2> : IRelationDataAccess<T1, T2>
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient = new HttpClient();
        /// <summary>
        /// The base URI
        /// </summary>
        private static readonly Uri baseUri = new Uri($"http://localhost:52286/api/{typeof(T1).Name}s");
        private const int timeOutMilliseconds = 8000;


        /// <summary>
        /// add entity to entity as an asynchronous operation.
        /// </summary>
        /// <param name="leftId">The left identifier.</param>
        /// <param name="rightId">The right identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<bool> AddEntityToEntityAsync(int leftId, int rightId)
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{leftId}/{typeof(T2).Name}s/{rightId}/create");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result;

            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(timeOutMilliseconds);
                result = await httpClient.PutAsync(uri, null, cts.Token);
            }

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// delete entity from entity as an asynchronous operation.
        /// </summary>
        /// <param name="leftId">The left identifier.</param>
        /// <param name="rightId">The right identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<bool> DeleteEntityFromEntityAsync(int leftId, int rightId)
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{leftId}/{typeof(T2).Name}s/{rightId}/delete");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result;

            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(timeOutMilliseconds);
                result = await httpClient.DeleteAsync(uri, cts.Token);
            }

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// get entities in entity as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>T2[].</returns>
        /// <exception cref="NetworkConnectionException"></exception>
        public async Task<T2[]> GetEntitiesInEntityAsync(int id, string param)
        {
            if (!InternetConnectionService.IsConnected())
                throw new NetworkConnectionException();

            var uri = new Uri($"{baseUri}/{id}/{param}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUserStorage.User.JwtToken);

            HttpResponseMessage result;

            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(timeOutMilliseconds);
                result = await httpClient.GetAsync(uri);
            }

            string json = await result.Content.ReadAsStringAsync();
            T2[] entities = JsonConvert.DeserializeObject<T2[]>(json);

            return entities;
        }
    }
}
