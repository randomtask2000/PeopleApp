// Routes
App.Router.map(function () {
    this.route('about');
    this.route('getpeople');
    this.resource('genders', function () {
        this.route('people', { path: '/:gender_name' });
    });
});

//// Use the application route to pre-load the list of people.
App.ApplicationRoute = Ember.Route.extend({
    model: function () {
        var store = this.get('store');
        var genders = ['Male', 'Female'].map(function (item) {
            return { name: item };
        });
        store.pushMany('gender', genders);
    }
});

App.GenresRoute = Ember.Route.extend({
    model: function () {
        return this.store.all('gender');
    },
});

// The gender.person route gets a genre as a model, but it uses the people controller.
// The route initializes the people controller with a list of people.

App.GroupsPeopleRoute = Ember.Route.extend({
    serialize: function (model) {
        return { gender_name: model.get('name') };
    },
    renderTemplate: function () {
        this.render({ controller: 'people' });
    },
    afterModel: function (gender) {
        // Use the group to get a list of people, and set the list on the controller.
        var controller = this.controllerFor('people');
        var store = controller.store;
        return store.findQuery('person', { gender: gender.get('name') })
            .then(function (data) {
                controller.set('model', data);
            });
    }
});