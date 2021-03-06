/*
 * File: app/view/maps.js
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

Ext.define('CWIC.view.maps', {
    extend: 'Ext.Container',
    alias: 'widget.maps',

    requires: [
        'Ext.Toolbar',
        'Ext.Button'
    ],

    config: {
        height: '100%',
        html: 'Test',
        itemId: 'maps',
        width: '100%',
        layout: 'fit',
        scrollable: false,
        items: [
            {
                xtype: 'toolbar',
                docked: 'bottom',
                layout: {
                    type: 'hbox',
                    pack: 'center'
                },
                items: [
                    {
                        xtype: 'button',
                        itemId: 'mapsHomeButton',
                        iconCls: 'home2'
                    },
                    {
                        xtype: 'button',
                        itemId: 'key',
                        text: 'Key'
                    },
                    {
                        xtype: 'button',
                        html: 'Legend',
                        itemId: 'legend'
                    },
                    {
                        xtype: 'button',
                        itemId: 'plantList',
                        text: 'Plants'
                    },
                    {
                        xtype: 'button',
                        itemId: 'mapsHelpButton',
                        text: '?'
                    }
                ]
            }
        ],
        listeners: [
            {
                fn: 'onKeyTap',
                event: 'tap',
                delegate: '#key'
            },
            {
                fn: 'onLegendTap',
                event: 'tap',
                delegate: '#legend'
            }
        ]
    },

    onKeyTap: function(button, e, eOpts) {
        var key = button.keyPanel;
        if (!key) {
            key = button.keyPanel = Ext.widget('key');
        }
        key.showBy(button);
    },

    onLegendTap: function(button, e, eOpts) {
        var legend = button.legendPanel;
        if (!legend) {
            legend = button.legendPanel = Ext.widget('legend');
        }
        legend.showBy(button);
    }

});