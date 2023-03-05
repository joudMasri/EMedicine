﻿using E_Medicine_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace E_Medicine_BE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        [HttpPost]
        [Route("registration")]
        public Response register(Users users) { 
            Response response = new Response();
            DAL dal = new DAL();
 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());

            response = dal.register(users, connection); 

            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response login(Users users) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.Login(users, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(Users users) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.viewUser(users,connection);
            return response;
        }

        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users users) {

            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());

            Response response = dal.updateProfile(users,connection);


            return response;
        }


    }
}
