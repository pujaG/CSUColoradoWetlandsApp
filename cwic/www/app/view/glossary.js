/*
 * File: app/view/glossary.js
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

Ext.define('CWIC.view.glossary', {
    extend: 'Ext.Container',
    alias: 'widget.glossary',

    requires: [
        'Ext.TitleBar',
        'Ext.Button',
        'Ext.dataview.List',
        'Ext.XTemplate',
        'Ext.dataview.IndexBar'
    ],

    config: {
        padding: 10,
        layout: 'fit',
        items: [
            {
                xtype: 'titlebar',
                docked: 'top',
                title: 'Glossary',
                items: [
                    {
                        xtype: 'button',
                        itemId: 'glossaryback',
                        ui: 'back',
                        text: 'Back'
                    }
                ]
            },
            {
                xtype: 'list',
                itemId: 'glossaryList',
                itemCls: 'sm',
                itemTpl: [
                    '<div><span class="sm"><b>{Name}</b>: {Definition}</span></div>'
                ],
                store: 'Glossary',
                grouped: true,
                indexBar: true
            }
        ]
    }

});