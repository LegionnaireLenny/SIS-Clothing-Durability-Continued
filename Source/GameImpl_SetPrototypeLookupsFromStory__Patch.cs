using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(GameImpl), "SetPrototypeLookupsFromStory")]
public class GameImpl_SetPrototypeLookupsFromStory__Patch
{
    private static void AddArmorWorkbenchRecipe(GameImpl __instance, EquipmentPrototype equip)
    {
        Recipe recipe = new Recipe();
        recipe.UniqueID = SISClothingDurability_Config.RepairArmorClothingRecipeUniqueId + "_" + equip.Name;
        bool flag = __instance.CurrentRecipes.ContainsKey(recipe.UniqueID);
        if (!flag)
        {
            recipe.NativeName = "Repair armor";
            recipe.ShowRecipe = ShowRecipe.IfAllIngredientsFound;
            recipe.CraftingStationType = "WorkBench";
            recipe.RecipeType = RecipeType.Workbench;
            recipe.SkillType = SkillType.Construction;
            recipe.SkillLevel = 2;
            recipe.ProductAmount = 1;
            recipe.IsDisassembly = false;
            recipe.ProductPrototypeName = equip.Name;
            recipe.SkillProgression = 1;
            recipe.CraftingTime = 6f;
            recipe.NameHash = StringUtil.JenkinsHash(SISClothingDurability_Config.RepairArmorClothingRecipeNameKey);
            Ingredient ingredient = new Ingredient();
            ingredient.PrototypeNames = new List<string>();
            ingredient.Amount = 1;
            ingredient.PrototypeNames.Add(equip.Name);
            Ingredient ingredient2 = new Ingredient();
            ingredient2.PrototypeNames = new List<string>();
            ingredient2.Amount = 2;
            ingredient2.PrototypeNames.Add("SISCD_HeavySewingKit");
            recipe.Ingredients.Add(ingredient);
            recipe.Ingredients.Add(ingredient2);
            __instance.CurrentStory.Recipes.Add(recipe.UniqueID, recipe);
            __instance.CurrentRecipes.Add(recipe.UniqueID, recipe);
        }
    }

    private static void AddSewingKitRecipe(GameImpl __instance, EquipmentPrototype equip)
    {
        bool flag = __instance.CurrentRecipes == null;
        if (!flag)
        {
            Recipe recipe = new Recipe();
            recipe.UniqueID = SISClothingDurability_Config.RepairNormalClothingRecipeUniqueId + "_" + equip.Name;
            bool flag2 = __instance.CurrentRecipes.ContainsKey(recipe.UniqueID);
            if (!flag2)
            {
                recipe.NativeName = "Repair clothing";
                recipe.ShowRecipe = ShowRecipe.IfAllIngredientsFound;
                recipe.SkillType = SkillType.Construction;
                recipe.SkillLevel = 0;
                recipe.ProductAmount = 1;
                recipe.IsDisassembly = false;
                recipe.ProductPrototypeName = equip.Name;
                recipe.SkillProgression = 1;
                recipe.CraftingTime = 2f;
                recipe.NameHash = StringUtil.JenkinsHash(SISClothingDurability_Config.RepairNormalClothingRecipeNameKey);
                Ingredient ingredient = new Ingredient();
                ingredient.PrototypeNames = new List<string>();
                ingredient.PrototypeNames.Add("SISCD_SewingKit");
                ingredient.Amount = 1;
                Ingredient ingredient2 = new Ingredient();
                ingredient2.PrototypeNames = new List<string>();
                ingredient2.Amount = 1;
                ingredient2.PrototypeNames.Add(equip.Name);
                ingredient2.Interchangeable = false;
                recipe.Ingredients.Add(ingredient2);
                recipe.Ingredients.Add(ingredient);
                __instance.CurrentRecipes.Add(recipe.UniqueID, recipe);
            }
        }
    }

    private static void AddRepairRecipes(GameImpl __instance, Story story)
    {
        foreach (EquipmentPrototype equipmentPrototype in __instance.CurrentEquipmentPrototypes.Values)
        {
            bool flag = SISClothingDurability_Utils.IsPrototypeNormalClothing(equipmentPrototype);
            if (flag)
            {
                GameImpl_SetPrototypeLookupsFromStory__Patch.AddSewingKitRecipe(__instance, equipmentPrototype);
            }
        }
    }

    private static void Postfix(GameImpl __instance, Story story)
    {
        GameImpl_SetPrototypeLookupsFromStory__Patch.AddRepairRecipes(__instance, story);
    }
}
