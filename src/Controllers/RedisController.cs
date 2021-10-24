
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace poc_asp_net_core_redis.Controllers
{
    [ApiExplorerSettings(GroupName = @"Redis")]
    [Route("api/redis")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        public RedisController(IDistributedCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{chave}")]
        public async Task<IActionResult> Salvar([FromRoute] string chave, [FromBody]object x)
        {
            if (string.IsNullOrEmpty(chave) || x == null) return BadRequest();

            DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

            _cache.SetString(chave, JsonConvert.SerializeObject(x), opcoesCache);

            return await Task.FromResult(Ok());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{chave}")]
        public async Task<IActionResult> Obter([FromRoute]string chave)
        {
            if (string.IsNullOrEmpty(chave)) return BadRequest();

            return await Task.FromResult(Ok(new { 
                key = chave,
                value = _cache.GetString(chave)
            }));
        }
    }
}
