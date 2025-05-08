<template>
    <el-container>
        <el-header v-loading="statistics.total === '...'" class="el-header-statistics">
            <el-row :gutter="15">
                <el-col :lg="24">
                    <el-card shadow="never">
                        <scStatistic :title="$t('总数')" :value="statistics.total" group-separator></scStatistic>
                    </el-card>
                </el-col>
            </el-row>
        </el-header>
        <el-header>
            <div class="left-panel">
                <na-search
                    :controls="[
                        {
                            type: 'input',
                            field: ['root', 'keywords'],
                            placeholder: $t('女朋友编号 / 女朋友名称'),
                            style: 'width:20rem',
                        },
                    ]"
                    :vue="this"
                    @reset="onReset"
                    @search="onSearch"
                    dateFormat="YYYY-MM-DD HH:mm:ss"
                    dateType="datetimerange"
                    dateValueFormat="YYYY-MM-DD HH:mm:ss"
                    ref="search" />
            </div>
            <div class="right-panel">
                <el-button @click="this.dialog.save = { mode: 'add' }" icon="el-icon-plus" type="primary"></el-button>
                <naButtonBulkDel :api="$API.adm_girlfriends.bulkDelete" :vue="this" />
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable
                :context-menus="['id', 'name', 'age', 'height', 'hobby', 'createdTime']"
                :params="query"
                :query-api="$API.adm_girlfriends.pagedQuery"
                :vue="this"
                @data-change="getStatistics"
                @selection-change="
                    (items) => {
                        selection = items
                    }
                "
                ref="table"
                remote-filter
                remote-sort
                row-key="id"
                stripe>
                <el-table-column type="selection" width="50" />
                <naColId :label="$t('女朋友编号')" prop="id" sortable="custom" width="170" />
                <el-table-column :label="$t('女朋友名称')" min-width="150" prop="name"  width="150"/>
                <el-table-column :label="$t('身高')" align="right" prop="height" width="150" sortable="custom">

                </el-table-column>
                <el-table-column :label="$t('年龄')" align="right" prop="age" sortable="custom" width="150" />
                <el-table-column :label="$t('爱好')" align="center" prop="hobby"  >
                </el-table-column>
                <naColOperation
                    :buttons="
                        naColOperation.buttons.concat(
                            {
                                icon: 'el-icon-document-copy',
                                confirm: true,
                                title: $t('复制女朋友'),
                                click: copyRole,
                            },
                            naColOperation.delButton(this.$t('删除女朋友'), $API.adm_girlfriends.delete),
                        )
                    "
                    :vue="this"
                    width="200" />
            </scTable>
        </el-main>
    </el-container>

    <save-dialog
        v-if="dialog.save"
        @closed="dialog.save = null"
        @mounted="$refs.saveDialog.open(dialog.save)"
        @success="(data, mode) => table.handleUpdate($refs.table, data, mode)"
        ref="saveDialog"></save-dialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import table from '@/config/table'
import naColOperation from '@/config/naColOperation'

const saveDialog = defineAsyncComponent(() => import('./save.vue'))
export default {
    components: {
        saveDialog,
    },
    computed: {
        naColOperation() {
            return naColOperation
        },
        table() {
            return table
        },
    },
    created() {},
    data() {
        return {
            statistics: {
                total: '...',
            },
            dialog: {},
            loading: false,
            query: {
                dynamicFilter: {
                    filters: [
                    ],
                },
                filter: {},
                keywords: this.keywords,
            },
            selection: [],
        }
    },
    inject: ['reload'],
    methods: {
        async getStatistics() {
            this.statistics.total = this.$refs.table?.total
        },
        async copyRole(row) {
            const loading = this.$loading()
            await this.$API.adm_girlfriends.create.post(Object.assign({}, row, { id: null, name: row.name + '-copy' }))
            this.$refs.table.refresh()
            loading.close()
        },
        //重置
        onReset() {
            // Object.entries(this.$refs.selectFilter.selected).forEach(([key, _]) => (this.$refs.selectFilter.selected[key] = ['']))

        },
        //搜索
        async onSearch(form) {
            if (Array.isArray(form.dy.createdTime)) {
                this.query.dynamicFilter.filters.push({
                    field: 'createdTime',
                    operator: 'dateRange',
                    value: form.dy.createdTime.map((x) => x.replace(/ 00:00:00$/, '')),
                })
            }
            if (typeof form.dy.Name === 'string' && form.dy.Name.trim() !== '') {
                this.query.dynamicFilter.filters.push({
                    field: 'name',
                    operator: 'eq',
                    value: form.dy.Name,
                })
            }
            console.log(this.query.dynamicFilter)
            await this.$refs.table.upData()
            console.log('222222222')
        },
    },
    async mounted() {
        if (this.keywords) {
            this.$refs.search.form.root.keywords = this.keywords
            this.$refs.search.keeps.push({
                field: 'keywords',
                value: this.keywords,
                type: 'root',
            })
        }

        this.onReset()
    },
    props: ['keywords'],
    watch: {},
}
</script>

<style scoped></style>