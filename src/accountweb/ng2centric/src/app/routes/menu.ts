const Dashboard = {
    name: 'Dashboard',
    link: '/dashboard',
    // iconclass: 'ion-aperture',
    imgpath: 'assets/img/icons/aperture.svg',
    order: 1,
    label: {
        count: 2,
        classname: 'badge bg-success'
    }
};

const Cards = {
    name: 'Cards',
    link: '/cards',
    order: 2,
    // iconclass: 'ion-radio-waves',
    imgpath: 'assets/img/icons/radio-waves.svg'
};

const Charts = {
    name: 'Charts',
    link: '/charts',
    // iconclass: 'ion-connection-bars',
    imgpath: 'assets/img/icons/connection-bars.svg',
    order: 3,
    subitems: [{
        name: 'Flot',
        link: '/charts/flot'
    }, {
        name: 'Radial',
        link: '/charts/radial'
    }]
};

const Forms = {
    name: 'Forms',
    link: '/forms',
    // iconclass: 'ion-clipboard',
    imgpath: 'assets/img/icons/clipboard.svg',
    order: 4,
    subitems: [{
        name: 'Classic',
        link: '/forms/classic'
    }, {
        name: 'Validation',
        link: '/forms/validation'
    }, {
        name: 'Advanced',
        link: '/forms/advanced'
    }, {
        name: 'Material',
        link: '/forms/material'
    }, {
        name: 'Editors',
        link: '/forms/editor'
    }, {
        name: 'Upload',
        link: '/forms/upload'
    }]
};

const Tables = {
    name: 'Tables',
    link: 'tables',
    order: 5,
    // iconclass: 'ion-navicon',
    imgpath: 'assets/img/icons/navicon.svg',
    subitems: [{
        name: 'Classic',
        link: '/tables/classic'
    }, {
        name: 'Datatables',
        link: '/tables/datatables'
    }]
};

const Layouts = {
    name: 'Layouts',
    link: 'layouts',
    order: 5.1,
    // iconclass: 'ion-grid',
    imgpath: 'assets/img/icons/grid.svg',
    subitems: [{
        name: 'Columns',
        link: '/layouts/columns'
    }, {
        name: 'Overlap',
        link: '/layouts/overlap'
    }, {
        name: 'Boxed',
        link: '/layouts/boxed'
    }, {
        name: 'Tabs Deep Link',
        link: '/layouts/tabs/home'
    }, {
        name: 'Containers',
        link: '/layouts/containers'
    }]
};

const Elements = {
    name: 'Elements',
    link: '/elements',
    // iconclass: 'ion-levels',
    imgpath: 'assets/img/icons/levels.svg',
    order: 6,
    subitems: [{
        name: 'Colors',
        link: '/elements/colors'
    }, {
        name: 'Whiteframes',
        link: '/elements/whiteframes'
    }, {
        name: 'Lists',
        link: '/elements/lists'
    }, {
        name: 'Bootstrapui',
        link: '/elements/bootstrapui'
    }, {
        name: 'Buttons',
        link: '/elements/buttons'
    }, {
        name: 'Sweet-alert',
        link: '/elements/sweetalert'
    }, {
        name: 'Spinners',
        link: '/elements/spinners'
    }, {
        name: 'Navtree',
        link: '/elements/navtree'
    }, {
        name: 'Grid',
        link: '/elements/grid'
    }, {
        name: 'Typography',
        link: '/elements/typography'
    }, {
        name: 'Icons',
        link: '/elements/icons'
    }, {
        name: 'Utilities',
        link: '/elements/utilities'
    }]
};

const Maps = {
    name: 'Maps',
    link: 'maps',
    // iconclass: 'ion-planet',
    imgpath: 'assets/img/icons/planet.svg',
    order: 7,
    subitems: [{
        name: 'Google Maps Full',
        link: '/maps/googlefull'
    }, {
        name: 'Google Maps',
        link: '/maps/google'
    }, {
        name: 'Vector Maps',
        link: '/maps/vector'
    }]
};

const Pages = {
    name: 'Pages',
    link: 'pages',
    order: 8,
    // iconclass: 'ion-ios-browsers',
    imgpath: 'assets/img/icons/ios-browsers.svg',
    subitems: [{
        name: 'Timeline',
        link: '/pages/timeline'
    }, {
        name: 'Invoice',
        link: '/pages/invoice'
    }, {
        name: 'Pricing',
        link: '/pages/pricing'
    }, {
        name: 'Contacts',
        link: '/pages/contacts'
    }, {
        name: 'FAQ',
        link: '/pages/faq'
    }, {
        name: 'Projects',
        link: '/pages/projects'
    }, {
        name: 'Blog',
        link: '/pages/blog'
    }, {
        name: 'Article',
        link: '/pages/article'
    }, {
        name: 'Profile',
        link: '/pages/profile'
    }, {
        name: 'Gallery',
        link: '/pages/gallery'
    }, {
        name: 'Wall',
        link: '/pages/wall'
    }, {
        name: 'Search',
        link: '/pages/search'
    }, {
        name: 'Messages Board',
        link: '/pages/messages'
    }]
};

const User = {
    name: 'User',
    link: 'user',
    order: 9,
    // iconclass: 'ion-person-stalker',
    imgpath: 'assets/img/icons/person-stalker.svg',
    subitems: [{
        name: 'Login',
        link: '/login'
    }, {
        name: 'Signup',
        link: '/signup'
    }, {
        name: 'Lock',
        link: '/lock'
    }, {
        name: 'Recover',
        link: '/recover'
    }]
};

export default [
    Dashboard,
    Cards,
    Charts,
    Forms,
    Tables,
    Layouts,
    Elements,
    Maps,
    Pages,
    User
];
