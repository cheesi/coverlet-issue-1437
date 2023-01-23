using CoverletMinimalReproducable1437.Tests.TestImplementation;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace CoverletMinimalReproducable1437.Tests;

public class TransactionBehaviorTests
{
    private readonly TransactionBehaviour<TestRequest, TestResponse> _sut;

    public TransactionBehaviorTests()
    {
        _sut = new TransactionBehaviour<TestRequest, TestResponse>();
    }

    [Fact]
    public async Task Calls_command_inside_transaction_scope_successfully()
    {
        //Arrange
        var request = new TestRequest();
        var requestHandlerDelegate = Substitute.For<RequestHandlerDelegate<TestResponse>>();
        var response = new TestResponse();
        requestHandlerDelegate.Invoke().Returns(Task.FromResult(response));

        //Act
        var result = await _sut.Handle(request, requestHandlerDelegate, default);

        //Assert
        result.Should().BeEquivalentTo(response);
    }
}