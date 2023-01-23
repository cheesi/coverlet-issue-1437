using CoverletMinimalReproducable1437.Tests.TestImplementation;
using FluentAssertions;

namespace CoverletMinimalReproducable1437.Tests;

public class UnhandledExceptionBehaviourTests
{
    private readonly UnhandledExceptionBehaviour<TestRequest, TestResponse> _sut;

    public UnhandledExceptionBehaviourTests()
    {
        _sut = new UnhandledExceptionBehaviour<TestRequest, TestResponse>();
    }

    [Fact]
    public async Task Calls_next_middleware()
    {
        //Arrange
        var request = new TestRequest();
        var response = new TestResponse();
        Task<TestResponse> handler() => Task.FromResult(response);

        //Act
        var result = await _sut.Handle(request, handler, default);

        //Assert
        result.Should().Be(response, because: "there is no actual action which prevents it");
    }

    [Fact]
    public async Task Rethrows_and_logs_exception()
    {
        //Arrange
        var request = new TestRequest();

        const string errorMessage = "Error";
        var exception = new ArgumentException(errorMessage);
        Task<TestResponse> handler() => throw exception;

        //Act
        var act = () => _sut.Handle(request, handler, default);

        //Assert
        await act.Should()
            .ThrowAsync<Exception>(because: "because a middleware has an unhandled exception")
            .WithMessage(errorMessage);
    }
}