import { IFood } from '../interfaces/ifood';

export class Food implements IFood{
  id: number;
  foodName: string;
  recipeId: number;
}
