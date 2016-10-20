App.Person = DS.Model.extend({
    ID: DS.attr(),
    LastName: DS.attr(),
    FirstName: DS.attr(),
    Birthdate: DS.attr(),
    Gender: DS.attr(),
    ImageFilename: DS.attr(),
    Age: DS.attr(),
    FLF: DS.attr(),
    FFF: DS.attr(),
});

// Ember Data expects a particular JSON payload format.
// This custom serializer convertss the data from Web API into the
// format that Ember Data expects.

App.PersonSerializer = DS.RESTSerializer.extend({
    // TODO: extractSingle: function (store, type, payload, id, requestType) {

    extractArray: function (store, type, payload, id, requestType) {
        var people = payload;
        payload = { people: people };
        return this._super(store, type, payload, id, requestType);
    },
    normalizeHash: {
        people: function (hash) {
            hash.id = hash.ID;
            delete hash.ID;
            return hash;
        }
    },
    serialize: function (post, options) {
        var json = {
            ID: post.get('ID'),
            LastName: post.get('LastName'),
            FirstName: post.get('FirstName'),
            Birthdate: post.get('Birthdate'),
            Gender: post.get('Gender'),
            ImageFilename: post.get('ImageFilename'),
            Age: post.get('Age'),
            FLF: post.get('FLF'),
            FFF: post.get('FFF')
        };
        return json;
    }
});

App.PersonAdapter = DS.RESTAdapter.extend({
    // Override how the REST adapter creates the JSON payload for PUT requests.
    updateRecord: function (store, type, record) {
        var data = store.serializerFor(type.typeKey).serialize(record);
        return this.ajax(this.buildURL(type.typeKey, data.ID), "PUT", { data: data });
    },
    namespace: 'api'
});

App.Gender = DS.Model.extend({
    name: DS.attr()
});