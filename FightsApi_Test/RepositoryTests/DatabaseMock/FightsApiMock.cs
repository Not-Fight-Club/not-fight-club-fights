using FightsApi_Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightsApi_Test.RepositoryTests.DatabaseMock
{
  public static class FightsApiMock
  {
    private static DbContextOptions<P3_NotFightClubContext> _opts =
      new DbContextOptionsBuilder<P3_NotFightClubContext>()
          .UseInMemoryDatabase("NotFightClubDB")
          .Options;

    public static P3_NotFightClubContext GetDbContext()
    {
      return new P3_NotFightClubContext(_opts);
    }
  }
}
