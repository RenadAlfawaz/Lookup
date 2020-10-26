using Microsoft.AspNetCore.Mvc;
using NIC.SBCPlatform.SharedModules.LookupManagement.Models.Test;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Controllers
{
    [Route("api/test")]
    public class TestController : LookupManagementController
    {

        public TestController()
        {

        }

        [HttpGet]
        [Route("")]
        public async Task<List<TestModel>> GetAsync()
        {
            return new List<TestModel>
            {
                new TestModel {Name = "Ahmed Graihy", BirthDate = new DateTime(1989, 10, 12)},
                new TestModel {Name = "Ahmed Hashem", BirthDate = new DateTime(1989, 10, 12)}
            };
        }
    }
}
