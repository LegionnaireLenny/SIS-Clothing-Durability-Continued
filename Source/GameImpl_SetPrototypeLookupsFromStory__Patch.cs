using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(GameImpl), "SetPrototypeLookupsFromStory")]
public class GameImpl_SetPrototypeLookupsFromStory__Patch
{
    private static void AddArmorWorkbenchRecipe(GameImpl __instance, EquipmentPrototype equip)
    {
        string uniqueID = SISClothingDurability_Config.RepairArmorClothingRecipeUniqueId + "_" + equip.Name;
        if (__instance.CurrentRecipes == null || __instance.CurrentRecipes.ContainsKey(uniqueID))
        {
            //SISClothingDurability_Config.Log($"Duplicate recipe found: {equip.Name}");
            return;
        }

        //SISClothingDurability_Config.Log($"Degradable armor found, adding recipe: {equip.Name}");
        Recipe recipe = new Recipe
        {
            UniqueID = uniqueID,
            NativeName = "Repair armor",
            ShowRecipe = ShowRecipe.IfAllIngredientsFound,
            CraftingStationType = "WorkBench",
            RecipeType = RecipeType.Workbench,
            SkillType = SkillType.Construction,
            SkillLevel = 2,
            ProductAmount = 1,
            IsDisassembly = false,
            ProductPrototypeName = equip.Name,
            SkillProgression = 1,
            CraftingTime = 6f,
            NameHash = StringUtil.JenkinsHash(SISClothingDurability_Config.RepairArmorClothingRecipeNameKey)
        };
        Ingredient ingredient = new Ingredient();
        ingredient.PrototypeNames = new List<string>();
        ingredient.Amount = 1;
        ingredient.PrototypeNames.Add(equip.Name);

        Ingredient ingredient2 = new Ingredient();
        ingredient2.PrototypeNames = new List<string>();
        ingredient2.Amount = 2;
        ingredient2.PrototypeNames.Add(SISClothingDurability_Config.HeavySewingKitNameKey);

        recipe.Ingredients.Add(ingredient);
        recipe.Ingredients.Add(ingredient2);
        __instance.CurrentStory.Recipes.Add(recipe.UniqueID, recipe);
        __instance.CurrentRecipes.Add(recipe.UniqueID, recipe);
    }

    private static void AddSewingKitRecipe(GameImpl __instance, Story story, EquipmentPrototype equip)
    {
        string uniqueID = SISClothingDurability_Config.RepairNormalClothingRecipeUniqueId + "_" + equip.Name;
        if (__instance.CurrentRecipes == null || __instance.CurrentRecipes.ContainsKey(uniqueID))
        {
            //SISClothingDurability_Config.Log($"Duplicate recipe found: {equip.Name}");
            return;
        }

        //SISClothingDurability_Config.Log($"Degradable clothing found, adding recipe: {equip.Name}");
        Recipe recipe = new Recipe
        {
            UniqueID = uniqueID,
            NativeName = "Repair clothing",
            ShowRecipe = ShowRecipe.IfAllIngredientsFound,
            SkillType = SkillType.Construction,
            SkillLevel = 0,
            ProductAmount = 1,
            IsDisassembly = false,
            ProductPrototypeName = equip.Name,
            SkillProgression = 1,
            CraftingTime = 2f,
            NameHash = StringUtil.JenkinsHash(SISClothingDurability_Config.RepairNormalClothingRecipeNameKey),
            Ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    PrototypeNames = new List<string>
                    {
                        SISClothingDurability_Config.SewingKitNameKey
                    },
                    Amount = 1
                },
                new Ingredient
                {
                    PrototypeNames = new List<string>
                    {
                        equip.Name
                    },
                    Amount = 1,
                    Interchangeable = false
                }
            }
        };

        story.Recipes.Add(recipe.UniqueID, recipe);
        __instance.CurrentRecipes.Add(recipe.UniqueID, recipe);
    }

    private static void Postfix(GameImpl __instance, Story story)
    {
        //SISClothingDurability_Config.Log($"Postfix - SetPrototypeLookupsFromStory - Searching equipment");
        foreach (EquipmentPrototype equipmentPrototype in __instance.CurrentEquipmentPrototypes.Values)
        {
            //SISClothingDurability_Config.Log($"Current equipment: {equipmentPrototype.Name}");
            if (SISClothingDurability_Utils.IsPrototypeDegradableClothing(equipmentPrototype))
            {
                GameImpl_SetPrototypeLookupsFromStory__Patch.AddSewingKitRecipe(__instance, story, equipmentPrototype);
            }
        }
    }
}
