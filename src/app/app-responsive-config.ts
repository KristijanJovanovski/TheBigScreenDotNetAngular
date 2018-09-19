import { ResponsiveConfig } from 'ngx-responsive'
let config = {
    breakPoints: {
        xs: {min: 200, max: 479},
        sm: {min: 480, max: 767},
        md: {min: 768, max: 1439},
        lg: {min: 1440, max: 2000},
        xl: {min: 2000}
    },
    debounceTime: 100 // allow to debounce checking timer
  };
  export function ResponsiveDefinitions(){
      return new ResponsiveConfig(config);
  }