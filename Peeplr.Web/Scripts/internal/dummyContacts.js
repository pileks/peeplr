window.preloaded = {};
window.preloaded.contacts = [
    {
        id: 1,
        firstName: 'Jure',
        lastName: 'Granic Skender',
        email: 'jure@dump.hr',
        streetAddress: 'Podluka 13a',
        city: 'Baska Voda',
        company: 'DUMP',
        tags: [{ id: 1, name: "developer" }, { id: 2, name: "sports" }],
        numbers: [{ type: 'home', numberString: '+38521620458' }, { type: 'mobile', numberString: '+385911614291' }]
    },
    {
        id: 2,
        firstName: 'Antonio',
        lastName: 'Pavlinovic',
        email: 'antonio@dump.hr',
        streetAddress: 'Neka lijeva 19',
        city: 'Kastel Sucurac',
        company: 'HR Cloud',
        tags: [{ id: 3, name: "designer"}, { id: 2, name: "sports"}],
        numbers: [{ type: 'home', numberString: '+38521231343' }, { type: 'mobile', numberString: '+385973456789' }]
    },
    {
        id: 3,
        firstName: 'Marko',
        lastName: 'Marinovic',
        email: 'markom@dump.hr',
        streetAddress: 'Ne sjecam se 22',
        city: 'Split',
        company: 'HR Cloud',
        tags: [{ id: 1, name: "developer" }, { id: 4, name: "gaming" }],
        numbers: [{ type: 'home', numberString: '+38521611110' }, { type: 'mobile', numberString: '+385915149878' }]
    }
];

window.preloaded.numberTypes = ['home', 'mobile'];

window.preloaded.tags = [
    { id: 1, name: "developer" },
    { id: 2, name: "sports" },
    { id: 3, name: "designer" },
    { id: 4, name: "gaming" }
];