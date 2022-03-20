using AutoMapper;
using ViewRecipes = View.Recipes;
using ModelRecipes = Model.Recipes;

namespace API.Mapping
{
    internal sealed class MappingProfile :Profile
    {
        public MappingProfile()
        {
            this.AllowNullCollections = true;

            this.CreateMap<ViewRecipes.Recipe, ModelRecipes.Recipe>(MemberList.None).ReverseMap();
            this.CreateMap<ViewRecipes.RecipeCreateInfo, ModelRecipes.RecipeCreateInfo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewRecipes.RecipeSearchInfo, ModelRecipes.RecipeSearchInfo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewRecipes.RecipeUpdateInfo, ModelRecipes.RecipeUpdateInfo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewRecipes.RecipesList, ModelRecipes.RecipesList>(MemberList.None).ReverseMap();
        }
    }
}
