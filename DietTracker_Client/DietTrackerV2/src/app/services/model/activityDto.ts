/**
 * DietTracker_Api
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

export interface ActivityDto { 
    id?: string;
    steps?: number;
    activeTime?: number;
    goalTime?: number;
    burnedCalories?: number;
    isDone?: boolean;
    date?: Date;
    distance?: number;
}