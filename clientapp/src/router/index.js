import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Details from '@/components/Details'
import Create from '@/components/Create'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/details/:address',
      name: 'Details',
      component: Details
    },
    {
      path: '/create',
      name: 'Create',
      component: Create
    }
  ]
})
