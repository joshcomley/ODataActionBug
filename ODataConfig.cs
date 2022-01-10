using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataActionBug.Controllers;

namespace ODataActionBug;

public class ODataConfig
{
    private static IEdmModel? _model;

    public static IEdmModel GetEdmModel()
    {
        if (_model != null)
        {
            return _model;
        }

        ODataConventionModelBuilder builder = new();
        
        var users = builder.EntitySet<User>("Users");

        // Works
        var hi = users.EntityType.Collection.Action(nameof(UsersController.Hi));
        hi.Returns<Hello>();

        // Doesn't work
        var hiSimpleFunction = users.EntityType.Collection.Function(nameof(UsersController.HiSimpleFunction));
        hiSimpleFunction.Returns<Hello>();
        hiSimpleFunction.Parameter<string>("name");

        // Doesn't work
        var hiSimple = users.EntityType.Collection.Action(nameof(UsersController.HiSimple));
        hiSimple.Returns<Hello>();
        hiSimple.Parameter<string>("name");

        // Doesn't work
        var hello = users.EntityType.Collection.Action(nameof(UsersController.Hello));
        hello.Returns<Hello>();
        hello.Parameter<SayHello>("request");

        // Doesn't work
        var goodbye = users.EntityType.Action(nameof(UsersController.Hello));
        goodbye.Returns<Hello>();
        goodbye.Parameter<SayHello>("request");

        return _model = builder.GetEdmModel();
    }
}