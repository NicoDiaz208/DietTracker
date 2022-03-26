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
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { WaterIntake } from '../model/waterIntake';
import { WaterIntakeCreationDto } from '../model/waterIntakeCreationDto';
import { WaterIntakeDto } from '../model/waterIntakeDto';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class WaterIntakeService {

    protected basePath = '/';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 
     * 
     * @param waterIntakeId 
     * @param activityId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiWaterIntakeAddActivityToWaterIntakePost(waterIntakeId?: string, activityId?: string, observe?: 'body', reportProgress?: boolean): Observable<boolean>;
    public apiWaterIntakeAddActivityToWaterIntakePost(waterIntakeId?: string, activityId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<boolean>>;
    public apiWaterIntakeAddActivityToWaterIntakePost(waterIntakeId?: string, activityId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<boolean>>;
    public apiWaterIntakeAddActivityToWaterIntakePost(waterIntakeId?: string, activityId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {



        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (waterIntakeId !== undefined && waterIntakeId !== null) {
            queryParameters = queryParameters.set('waterIntakeId', <any>waterIntakeId);
        }
        if (activityId !== undefined && activityId !== null) {
            queryParameters = queryParameters.set('activityId', <any>activityId);
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<boolean>('post',`${this.basePath}/api/WaterIntake/AddActivityToWaterIntake`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiWaterIntakeGet(observe?: 'body', reportProgress?: boolean): Observable<Array<WaterIntakeDto>>;
    public apiWaterIntakeGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<WaterIntakeDto>>>;
    public apiWaterIntakeGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<WaterIntakeDto>>>;
    public apiWaterIntakeGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<WaterIntakeDto>>('get',`${this.basePath}/api/WaterIntake`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param date 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiWaterIntakeGetByDateGet(date?: Date, observe?: 'body', reportProgress?: boolean): Observable<WaterIntake>;
    public apiWaterIntakeGetByDateGet(date?: Date, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<WaterIntake>>;
    public apiWaterIntakeGetByDateGet(date?: Date, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<WaterIntake>>;
    public apiWaterIntakeGetByDateGet(date?: Date, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (date !== undefined && date !== null) {
            queryParameters = queryParameters.set('date', <any>date.toISOString());
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<WaterIntake>('get',`${this.basePath}/api/WaterIntake/GetByDate`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiWaterIntakePost(body?: WaterIntakeCreationDto, observe?: 'body', reportProgress?: boolean): Observable<WaterIntakeDto>;
    public apiWaterIntakePost(body?: WaterIntakeCreationDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<WaterIntakeDto>>;
    public apiWaterIntakePost(body?: WaterIntakeCreationDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<WaterIntakeDto>>;
    public apiWaterIntakePost(body?: WaterIntakeCreationDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<WaterIntakeDto>('post',`${this.basePath}/api/WaterIntake`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param body 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiWaterIntakeReplacePost(body?: WaterIntakeDto, id?: string, observe?: 'body', reportProgress?: boolean): Observable<WaterIntakeDto>;
    public apiWaterIntakeReplacePost(body?: WaterIntakeDto, id?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<WaterIntakeDto>>;
    public apiWaterIntakeReplacePost(body?: WaterIntakeDto, id?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<WaterIntakeDto>>;
    public apiWaterIntakeReplacePost(body?: WaterIntakeDto, id?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {



        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (id !== undefined && id !== null) {
            queryParameters = queryParameters.set('id', <any>id);
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<WaterIntakeDto>('post',`${this.basePath}/api/WaterIntake/Replace`,
            {
                body: body,
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getSingleWaterIntake(id: string, observe?: 'body', reportProgress?: boolean): Observable<WaterIntakeDto>;
    public getSingleWaterIntake(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<WaterIntakeDto>>;
    public getSingleWaterIntake(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<WaterIntakeDto>>;
    public getSingleWaterIntake(id: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling getSingleWaterIntake.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<WaterIntakeDto>('get',`${this.basePath}/api/WaterIntake/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
