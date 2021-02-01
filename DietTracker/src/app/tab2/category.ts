import { Recipe } from './recipe';

export class Category {
    private name : string;
    private recipes : Array<Recipe>;

    constructor( v_name : string, v_recipes : Array<Recipe>){
        this.name = v_name;
        this.recipes = v_recipes;
    }
    
    getRecipes(){
        return this.recipes;
    }

    getRecipesCount(){
        return this.recipes.length;
    }
}
