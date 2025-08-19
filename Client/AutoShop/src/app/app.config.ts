import { APP_INITIALIZER, ApplicationConfig, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { InitService } from './core/services/init.service';
import { lastValueFrom } from 'rxjs';
import { authInterceptor } from './core/interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)
    , provideAnimationsAsync(),
  provideHttpClient(withInterceptors([errorInterceptor,loadingInterceptor,authInterceptor])),
  provideAppInitializer(()=>{
    const init=inject(InitService);
    return lastValueFrom(init.init()).finally(()=>{
      const splash=document.getElementById("splash");
      if(splash){
        splash.remove();
      }
    }
      
    );
  })
   
],
 
};
