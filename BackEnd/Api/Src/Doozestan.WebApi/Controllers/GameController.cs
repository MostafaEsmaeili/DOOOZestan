using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Doozestan.Api;
using Doozestan.Common.WcfService;
using Doozestan.Domain;
using Doozestan.Domain.Enum;
using Doozestan.Domain.ServiceResponse;
using Framework.Logging;

namespace Doozestan.WebApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        public CustomLogger Logger => new CustomLogger(GetType().FullName);
        public GameProvider GameProvider=new GameProvider();

        [System.Web.Http.Route("{gameId}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetGameDetails([FromUri]int gameId)
        {
            var response = new ResponseDto<Game>();
            try
            {
                var result = GameProvider.GetGames(gameId);
                response.Data = result;
                response.ResponseStatus = ApiResponseStatus.Ok;
                return Json(response);
            }
          
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                response.ResponseStatus = ApiResponseStatus.Error;
                return BadRequest();
            }

         //   return Json(response);
        }


        /// <summary>
        /// Get All Game Information
        /// </summary>
        /// <returns>
        /// Game Entity :
        ///  public int Id { get; set; }
        ///Title 
        ///  StartDate 
        ///    EndDate 
        ///   Status 
        ///   WinnerId 
        /// </returns>
        /// <response code="400">Bad request</response>
        /// <remarks>Get All Game Information</remarks>
        [System.Web.Http.Route("")]
        [System.Web.Http.HttpGet]
       
        public IHttpActionResult GetAllGame()
        {
            var response = new ResponseDto<IQueryable<Game>>();
            try
            {
                var result = GameProvider.GameService.Queryable();
                response.Data = result;
                response.ResponseStatus = ApiResponseStatus.Ok;
                return Json(response);
            }

            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                response.ResponseStatus = ApiResponseStatus.Error;
                return BadRequest();
            }

            //   return Json(response);
        }
        /// <summary>
        /// using for Adding a gmae
        /// </summary>
        /// <param name="game">
        /// just  Staus
        /// Status for start game is 1
        /// </param>
        /// <returns>game with id </returns>
        /// <response code="200">Update Done</response>
        /// <response code="400">Bad request Looj Logs for more Information</response>
        [System.Web.Http.Route("AddNewGame")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult AddnewGame([FromBody]Game game)
        {
            var response = new ResponseDto<Game>();
            try
            {
                var result = GameProvider.addNewGame(game);
                if (result.Id>0)
                {
                    response.Data = result;
                    response.ResponseStatus = ApiResponseStatus.Ok;
                    return Json(response);
                }
                return BadRequest("خطا در انجام عملیات");
            }

            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                response.ResponseStatus = ApiResponseStatus.Error;
                return BadRequest();
            }

            //   return Json(response);
        }

        /// <summary>
        /// using for finished the gmae
        /// </summary>
        /// <param name="game">
        /// just need to fill WinnerId and Staus
        /// Status for end game is 2
        /// </param>
        /// <returns>nothing just 200 http code </returns>
        /// <response code="200">Update Done</response>
        /// <response code="400">Bad request Looj Logs for more Information</response>
        [System.Web.Http.Route("UpdateGame")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateGame([FromBody]Game game)
        {
            var response = new ResponseDto<Game>();
            try
            {
                 GameProvider.UpdateGame(game);
               
                    response.Data = GameProvider.GetGames(game.Id);
                    response.ResponseStatus = ApiResponseStatus.Ok;
                   return Json(response);
               
            }

            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                response.ResponseStatus = ApiResponseStatus.Error;
                return BadRequest();
            }

            //   return Json(response);
        }
    }
}
