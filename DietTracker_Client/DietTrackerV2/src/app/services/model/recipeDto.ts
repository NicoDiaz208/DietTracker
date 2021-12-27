/**
 * DietTrackerApi
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { CategoryDto } from './categoryDto';
import { IngredientDto } from './ingredientDto';

export interface RecipeDto { 
    id?: string;
    name?: string;
    prepareTime?: string;
    difficulty?: number;
    category?: Array<CategoryDto>;
    preparation?: string;
    foodIds?: Array<IngredientDto>;
}