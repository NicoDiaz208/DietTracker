import { INutritionFacts } from '../interfaces/inutrition-facts';

export class NutritionFacts implements INutritionFacts{
  id: number;
  calories: number;
  protein: number;
  totalCarbs: number;
  suggar: number;
  fiber: number;
  fat: number;
}
