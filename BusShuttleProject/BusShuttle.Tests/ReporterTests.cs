namespace BusShuttle.Tests;

using BusShuttle;
using System;

public class ReporterTests
{
    
    List<PassengerData> sampleData;

    public ReporterTests() {
        
        sampleData = new List<PassengerData>();
        
    }

    [Fact]
    public void Test_FindBusiestStop_Just2Stops()
    {
        Stop sampleStop = new Stop("MyStop");
        Loop sampleLoop = new Loop("MyLoop");
        Driver sampleDriver = new Driver("Sample");
        PassengerData samplePassengerData = new PassengerData(
            5,
            sampleStop,
            sampleLoop,
            sampleDriver
        );
        sampleData.Add(samplePassengerData);

        Stop sampleStop2 = new Stop("MyStop2");
        PassengerData samplePassengerData2 = new PassengerData(
            6,
            sampleStop2,
            sampleLoop,
            sampleDriver
        );
        sampleData.Add(samplePassengerData2);

        var result = Reporter.FindBusiestStop(sampleData);
        Assert.Equal("MyStop2", result.Name);
    }
    
    [Fact]
    public void Test_FindBusiestStop_MoreData() {
        sampleData.Add(new PassengerData(4, new Stop("MyStop1"), new Loop("Loop1"), new Driver("Driver1")));
        sampleData.Add(new PassengerData(5, new Stop("MyStop2"), new Loop("Loop1"), new Driver("Driver1")));
        sampleData.Add(new PassengerData(2, new Stop("MyStop1"), new Loop("Loop1"), new Driver("Driver1")));
        var result = Reporter.FindBusiestStop(sampleData);
        Assert.Equal("MyStop1", result.Name);
    }
}