using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DafDev.Katas.MarsRover.Web.Tests.Data;
public class RoverTestData
{
    public static IEnumerable<object[]> GetForwardCommandData()
    {
        yield return new object[] { 'N', 0, 1 };
        yield return new object[] { 'E', 1, 0 };
        yield return new object[] { 'S', 0, -1 };
        yield return new object[] { 'W', -1, 0 };
    }

    public static IEnumerable<object[]> GetBackwardCommandData()
    {
        yield return new object[] { 'N', 0, -1 };
        yield return new object[] { 'E', -1, 0 };
        yield return new object[] { 'S', 0, 1 };
        yield return new object[] { 'W', 1, 0 };
    }

    public static IEnumerable<object[]> GetTurnRightCommandData()
    {
        yield return new object[] { 'N', 'E' };
        yield return new object[] { 'E', 'S' };
        yield return new object[] { 'S', 'W' };
        yield return new object[] { 'W', 'N' };
    }

    public static IEnumerable<object[]> GetTurnLeftCommandData()
    {
        yield return new object[] { 'N', 'W' };
    }
}
