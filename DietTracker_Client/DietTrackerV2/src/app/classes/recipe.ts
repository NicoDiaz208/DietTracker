import { IRecipe } from '../interfaces/irecipe';

export class Recipe implements IRecipe {
  id: number;
  prepareTime: string;
  difficulty: string;
  category: string;
  createdDate: string;
}
