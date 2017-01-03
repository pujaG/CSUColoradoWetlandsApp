/*
 * File: app/controller/plantList.js
 *
 * This file was generated by Sencha Architect version 3.0.4.
 * http://www.sencha.com/products/architect/
 *
 * This file requires use of the Sencha Touch 2.3.x library, under independent license.
 * License of Sencha Architect does not include license for Sencha Touch 2.3.x. For more
 * details see http://www.sencha.com/license or contact license@sencha.com.
 *
 * This file will be auto-generated each and everytime you save your project.
 *
 * Do NOT hand edit this file.
 */

Ext.define('CWIC.controller.plantList', {
    extend: 'Ext.app.Controller',

    requires: [
        'Ext.MessageBox'
    ],

    config: {
        views: [
            'plantList'
        ],

        refs: {
            fieldGuide: '#fieldguide',
            plantProfile: 'plantprofile',
            plantList: 'list[id=plantlist]'
        },

        control: {
            "button": {
                tap: 'onButtonTap'
            },
            "plantlist > list": {
                itemtap: 'onItemTap'
            },
            "#selectsort": {
                change: 'onSortChange'
            }
        }
    },

    onButtonTap: function(button, e, eOpts) {
        if (button.config.itemId === "filterButton") {
            this.getFieldGuide().animateActiveItem(CWIC.util.Cwic.filterTab,  {type: 'slide', direction: 'up'});
        }
        else if (button.config.itemId === "glossaryButton"){
            if (Ext.getStore('Glossary').getCount()===0) Ext.getStore('Glossary').addData(CWIC.util.Glossary.glossary);

            this.getFieldGuide().animateActiveItem(CWIC.util.Cwic.glossaryTab,  {type: 'slide', direction: 'up'});
        }
        else if (button.config.itemId === "helpButton"){
            //debugger;
            var fieldGuide = this.getFieldGuide();

            fieldGuide.down('#help').setHtml(CWIC.util.Cwic.helpText);

            fieldGuide.animateActiveItem(CWIC.util.Cwic.helpTab,  {type: 'slide', direction: 'up'});
        }
    },

    onItemTap: function(dataview, index, target, record, e, eOpts) {
        var me = this,
            form = me.getPlantProfile();

        form.setupPlantProfile(record,index,"list");

        form.setActiveItem(0); //To make the images carousel the first tab

        //Load the plantProfile tab (now that we've set it all up)
        me.getFieldGuide().animateActiveItem(CWIC.util.Cwic.plantProfileTab, {
            type: 'slide',
            direction: 'left'
        });

    },

    onSortChange: function(selectfield, newValue, oldValue, eOpts) {
        //debugger;

        if(newValue ==="CommonName")
        {
            this.getPlantList().setItemTpl(CWIC.util.Cwic.sortCommonTemplate);
        }
        else if (newValue === "Family")
        {
            this.getPlantList().setItemTpl(CWIC.util.Cwic.sortFamilyTemplate);
        }
        else if (newValue === "Sections")
        {
            this.getPlantList().setItemTpl(CWIC.util.Cwic.sortSectionsTemplate);
        }
        else if (newValue === "SciNameAuthor")
        {
            this.getPlantList().setItemTpl(CWIC.util.Cwic.sortSciNameTemplate);
        }
        this.getPlantList().getStore().sort([
            {
                property: newValue,
                direction: 'ASC'
            },
            {
                property: 'SciNameAuthor',
                direction: 'ASC'
            }
        ]);



    }

});