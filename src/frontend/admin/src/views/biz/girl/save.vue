<template>
    <scDialog v-model="visible" :title="`${titleMap[mode]}：${form?.id ?? '...'}`" @closed="$emit('closed')" destroy-on-close full-screen>
        <div v-loading="loading">
            <el-tabs v-model="tabId" tab-position="top">
                <el-tab-pane :label="$t('基本信息')">
                    <el-form :disabled="mode === 'view'" :model="form" :rules="rules" label-width="15rem" ref="dialogForm">
                        <el-form-item :label="$t('女朋友名称')" prop="name">
                            <el-input v-model="form.name" clearable></el-input>
                        </el-form-item>
                        <el-form-item :label="$t('年龄')" prop="age">
                            <el-input-number v-model="form.age" :min="0" controls-position="right" style="width: 100%"></el-input-number>
                        </el-form-item>
                        <el-form-item :label="$t('身高')" prop="height">
                            <el-input-number v-model="form.height" :min="0" controls-position="right" style="width: 100%"></el-input-number>
                        </el-form-item>
                        <el-form-item :label="$t('爱好')" prop="hobby">
                            <el-input v-model="form.hobby" clearable type="textarea"></el-input>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
            </el-tabs>
        </div>
        <template #footer>
            <el-button @click="visible = false">{{ $t('取消') }}</el-button>
            <el-button v-if="mode !== 'view'" :disabled="loading" :loading="loading" @click="submit" type="primary">{{ $t('保存') }}</el-button>
        </template>
    </scDialog>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import vkbeautify from 'vkbeautify/index'
import config from '@/config'

const User = defineAsyncComponent(() => import('@/views/sys/user/index.vue'))
export default {
    components: { User },
    created() {},
    data() {
        return {
            //表单数据
            form: {  age: 18, height: 168 ,hobby:"阿巴阿巴"},
            loading: true,
            mode: 'add',
            //验证规则
            rules: {
                sort: [
                    {
                        required: true,
                        message: '请输入排序',
                        trigger: 'change',
                    },
                ],
                name: [{ required: true, message: '请输入女朋友名称' }],
            },
            tabId: '0',
            titleMap: {
                add: this.$t('新增女朋友'),
                edit: this.$t('编辑女朋友'),
                view: this.$t('查看女朋友'),
            },
            trees: {
                menu: [],
                api: [],
                dept: [],
            },
            visible: false,
        }
    },
    emits: ['success', 'closed', 'mounted'],
    methods: {

        //显示
        async open(data) {
            this.visible = true
            this.loading = true
            this.mode = data.mode
            if (data.row?.id) {
                const res = await this.$API.adm_girlfriends.get.post({ id: data.row.id })
                console.log(res.data)
                Object.assign(this.form, res.data)
            }
            this.loading = false
            if (data.tabId) {
                this.tabId = data.tabId
            }
            return this
        },

        //表单提交方法
        async submit() {
            const valid = await this.$refs.dialogForm.validate().catch(() => {})
            if (!valid) {
                return false
            }
            this.loading = true
            const  postData=this.form
            const method = this.mode === 'add' ? this.$API.adm_girlfriends.create : this.$API.adm_girlfriends.edit
            try {
                console.log(postData)
                const res = await method.post(postData)
                this.$emit('success', res.data, this.mode)
                this.visible = false
                this.$message.success(this.$t('操作成功'))
            } catch {}
            this.loading = false
        },
    },
    mounted() {
        this.$emit('mounted')
    },
}
</script>

<style scoped></style>