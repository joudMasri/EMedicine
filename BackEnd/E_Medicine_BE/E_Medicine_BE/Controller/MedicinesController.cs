using E_Medicine_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace E_Medicine_BE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public MedicinesController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.addToCart(cart, connection);
            return response;
        }


        [HttpPost]
        [Route("placeOrder")]
        public Response placeOder(Users users) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.placeOrder(users, connection);
            return response;
        }

        [HttpPost]
        [Route("orderList")]
        public Response orderList(Users users) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.orderList(users, connection);
            return response;
        }



    }
}
