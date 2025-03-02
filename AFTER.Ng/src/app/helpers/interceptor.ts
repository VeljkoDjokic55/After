import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from "@angular/common/http";
import { Observable, of, throwError } from "rxjs";
import { Injectable } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { ToastrComponentlessModule, ToastrService } from "ngx-toastr";
@Injectable()
export class HttpInterceptorService implements HttpInterceptor {


  public static methodsToProcess = ['GET', 'POST', 'PUT', 'DELETE'];
  public static showToastMomentaneously = true;
  public static successfulResponses = [201, 200];
  public static errorResponses = [400, 401, 403, 404, 405];
  public static serverErrorResponses = [500, 504];

  constructor(private toastr: ToastrService) { }
  intercept(req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

    const idToken = localStorage.getItem('token');

    if (idToken) {
      req = req.clone({
        headers: req.headers.set('Authorization',
          'Bearer ' + idToken)
      });
    }
    return next.handle(req).pipe(tap(
      evt => {
        if (evt instanceof HttpResponse) {
        }
      }
    ),
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          if(err.status == 401 && !err.error)
            this.toastr.error('Session timed out. Please log in.');
        }
        return throwError(err);
      }));
  }
}
