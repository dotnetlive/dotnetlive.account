import { LayoutComponent } from '../layout/layout.component';

import * as dashboard from './dashboard';
import * as cards from './cards';
import * as elements from './elements';
import * as forms from './forms';
import * as layouts from './layouts';
import * as user from './user';
import * as tables from './tables';
import * as pages from './pages';
import * as maps from './maps';
import * as charts from './charts';

const routes = [

    {
        path: '',
        component: LayoutComponent,
        children: [

            { path: '', redirectTo: 'dashboard' },
            { path: 'dashboard', component: dashboard.DashboardComponent },
            { path: 'cards', component: cards.CardsComponent },

            {
                path: 'charts',
                children: [
                    { path: 'radial', component: charts.RadialComponent },
                    { path: 'flot', component: charts.FlotComponent }
                ]
            },

            {
                path: 'forms',
                children: [
                    { path: 'classic', component: forms.ClassicComponent },
                    { path: 'validation', component: forms.ValidationComponent },
                    { path: 'advanced', component: forms.AdvancedComponent },
                    { path: 'material', component: forms.MaterialComponent },
                    { path: 'editor', component: forms.EditorsComponent },
                    { path: 'upload', component: forms.UploadComponent }
                ]
            },

            {
                path: 'tables',
                children: [
                    { path: 'classic', component: tables.ClassicComponent },
                    { path: 'datatables', component: tables.DatatablesComponent }
                ]
            },

            {
                path: 'layouts',
                children: [
                    { path: 'boxed', component: layouts.BoxedComponent },
                    { path: 'columns', component: layouts.ColumnsComponent },
                    { path: 'containers', component: layouts.ContainersComponent },
                    { path: 'overlap', component: layouts.OverlapComponent },
                    {
                        path: 'tabs',
                        component: layouts.TabsComponent,
                        children: [
                            // { path: '', redirectTo: 'home' },
                            { path: 'home', component: layouts.TabhomeComponent },
                            { path: 'profile', component: layouts.TabprofileComponent },
                            { path: 'message', component: layouts.TabmessageComponent }
                        ]
                    }
                ]
            },

            {
                path: 'elements',
                children: [
                    { path: 'bootstrapui', component: elements.BootstrapuiComponent },
                    { path: 'buttons', component: elements.ButtonsComponent },
                    { path: 'colors', component: elements.ColorsComponent },
                    { path: 'grid', component: elements.GridComponent },
                    { path: 'icons', component: elements.IconsComponent },
                    { path: 'lists', component: elements.ListsComponent },
                    { path: 'navtree', component: elements.NavtreeComponent },
                    { path: 'spinners', component: elements.SpinnersComponent },
                    { path: 'sweetalert', component: elements.SweetalertComponent },
                    { path: 'typography', component: elements.TypographyComponent },
                    { path: 'utilities', component: elements.UtilitiesComponent },
                    { path: 'whiteframes', component: elements.WhiteframesComponent }
                ]
            },

            {
                path: 'maps',
                children: [
                    { path: 'googlefull', component: maps.GooglefullComponent },
                    { path: 'google', component: maps.GoogleComponent },
                    { path: 'vector', component: maps.VectorComponent }
                ]
            },

            {
                path: 'pages',
                children: [
                    { path: 'article', component: pages.ArticleComponent },
                    { path: 'blog', component: pages.BlogComponent },
                    { path: 'contacts', component: pages.ContactsComponent },
                    { path: 'faq', component: pages.FaqComponent },
                    { path: 'gallery', component: pages.GalleryComponent },
                    { path: 'invoice', component: pages.InvoiceComponent },
                    { path: 'messages', component: pages.MessagesComponent },
                    { path: 'pricing', component: pages.PricingComponent },
                    { path: 'profile', component: pages.ProfileComponent },
                    { path: 'projects', component: pages.ProjectsComponent },
                    { path: 'search', component: pages.SearchComponent },
                    { path: 'timeline', component: pages.TimelineComponent },
                    { path: 'wall', component: pages.WallComponent },
                ]
            }

        ]
    },

    { path: 'login', component: user.LoginComponent },
    { path: 'signup', component: user.SignupComponent },
    { path: 'lock', component: user.LockComponent },
    { path: 'recover', component: user.RecoverComponent },

    // Not found
    { path: '**', redirectTo: 'home' }

];

export default routes;
